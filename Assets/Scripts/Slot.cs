using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");

        if (transform.childCount <= 0) return;
        var otherCardTransform = eventData.pointerDrag.transform;
        var cardTransform = transform.GetChild(0);

        var otherCardComponent = otherCardTransform.GetComponent<Card>();
        var cardComponent = cardTransform.GetComponent<Card>();

        if (!cardComponent.CheckLocked() && !otherCardComponent.CheckLocked())
        {
            cardTransform.SetParent(otherCardTransform.parent);
            cardTransform.localPosition = Vector3.back;

            otherCardTransform.SetParent(transform);
            otherCardTransform.localPosition = Vector3.back;
            
            EventManager.SendSlotChanged();
        }
    }
}
