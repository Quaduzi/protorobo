using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _collider.enabled = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var hitPos = eventData.pointerCurrentRaycast.worldPosition;
        transform.position = new Vector3(hitPos.x, hitPos.y, -1);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        var behind = eventData.pointerCurrentRaycast.gameObject;
        if (behind != null)
        {
            Debug.Log(behind.name);
            if (behind.TryGetComponent<Card>(out var targetCard))
            {
                SwapCards(this, targetCard);
            }

            if (behind.TryGetComponent<Slot>(out var targetSlot))
            {
                transform.SetParent(targetSlot.transform);
            }
        }
        transform.localPosition = Vector3.back;
        _collider.enabled = true;
    }
    
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
