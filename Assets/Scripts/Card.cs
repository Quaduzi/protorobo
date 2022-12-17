using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, 
        IBeginDragHandler, IEndDragHandler, IPointerEnterHandler,
        IPointerExitHandler
{
    public bool cardLocked = false;
    public CardLockType lockType = CardLockType.Unlock;

    private Transform _transform;
    private int _defaultLayer;
    private bool _cardDragged;

    public enum CardLockType
    {
        Unlock,
        KeyLock,
        AbsoluteLock
    }
    
    void Start()
    {
        _transform = GetComponent<Transform>();
        _defaultLayer = _transform.gameObject.layer;
        
        LockCard();
    }

    private void Awake()
    {
        EventManager.OnStartTransition.AddListener(LockCard);
        EventManager.OnEndTransition.AddListener(UnlockCard);
        EventManager.OnGameStarted.AddListener(UnlockCard);
    }

    private void LockCard()
    {
        cardLocked = true;
    }

    private void UnlockCard()
    {
        cardLocked = false;
    }

    public bool CheckLocked() => cardLocked || lockType != CardLockType.Unlock;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckLocked()) return;
        _cardDragged = true;
        EventManager.SendCardBeginDrag();
        _transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_cardDragged && !CheckLocked()) OnBeginDrag(null);
        if (CheckLocked() || !_cardDragged) return;
        var hitPos = eventData.pointerCurrentRaycast.worldPosition;
        _transform.position = new Vector3(hitPos.x, hitPos.y, - 5);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _cardDragged = false;
        if (CheckLocked()) return;
        
        EventManager.SendCardEndDrag();
        _transform.localPosition = Vector3.back;
       _transform.gameObject.layer = _defaultLayer;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
    }
    
}
