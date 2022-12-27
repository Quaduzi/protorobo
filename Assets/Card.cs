using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(SpriteMask))]
public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Collider2D _collider;
    private SpriteMask _spriteMask;
    [SerializeField] private bool locked;
    public bool IsLocked => locked;
    

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteMask = GetComponent<SpriteMask>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsLocked) return;
        _collider.enabled = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (IsLocked) return;
        var hitPos = eventData.pointerCurrentRaycast.worldPosition;
        transform.position = new Vector3(hitPos.x, hitPos.y, -1);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsLocked) return;
        var behind = eventData.pointerCurrentRaycast.gameObject;
        if (behind != null)
        {
            Debug.Log(behind.name);
            if (behind.TryGetComponent<Card>(out var targetCard))
            {
                if (!targetCard.IsLocked) SwapCards(this, targetCard);
            }

            if (behind.TryGetComponent<Slot>(out var targetSlot))
            {
                transform.SetParent(targetSlot.transform);
            }
        }
        transform.localPosition = Vector3.back;
        _collider.enabled = true;
    }

    public void BlockCard() => locked = true;
    public void UnblockCard() => locked = false;
    public void EnableSpriteMask() => _spriteMask.enabled = true;
    public void DisableSpriteMask() => _spriteMask.enabled = false;

    private void SwapCards(Card a, Card b)
    {
        var cardA = a.transform;
        var cardB = b.transform;
        var slotA = cardA.parent;
        var slotB = cardB.parent;
        cardA.SetParent(slotB);
        cardA.localPosition = Vector3.back;
        cardB.SetParent(slotA);
        cardB.localPosition = Vector3.back;
    }
}
