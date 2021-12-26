using UnityEngine;
using UnityEngine.EventSystems;

public class CardToPieceDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Slot _slot;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_slot.Empty()) return;
        _slot.Piece.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_slot.Empty()) return;
        _slot.Piece.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_slot.Empty()) return;
        _slot.Piece.OnEndDrag(eventData);
    }
}
