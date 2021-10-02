using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public UnityEvent OnMovedToSlotEvent;
    [HideInInspector] public UnityEvent OnCancelMoveEvent;
    [HideInInspector] public UnityEvent OnReturnedHomeEvent;
    
    public Minister Minister => _minister;
    
    public bool IsHome => _homeSlot == _currentSlot;
    public bool OnFinishedSlot => _currentSlot.IsClosed;
    
    [SerializeField] private Slot _homeSlot;
    [SerializeField] private Minister _minister;
    
    private GraphicRaycaster _graphicRaycaster;
    private Transform _canvasTransform;
    private Slot _currentSlot;
    
    private void Awake()
    {
        _graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        _canvasTransform = GetComponentInParent<Canvas>().transform;
        _minister ??= GetComponent<Minister>();
    }

    private void Start()
    {
        if (_homeSlot != null)
        {
            _homeSlot.AssignAsHome(this);
            PutIntoSlot(_homeSlot);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_minister.CanAct) return;
        
        transform.SetParent(_canvasTransform);   // Parent ourselves to canvas
        transform.SetAsLastSibling();           // put us at the end to render on top of everything
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!_minister.CanAct) return;
        
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_minister.CanAct) return;
        
        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out Slot slot))
            {
                if (slot.IsHomeSlot && slot != _homeSlot)
                {
                    ReturnHome();
                    OnMovedToSlotEvent.Invoke();
                    return;
                }

                if (slot.CanPutPiece(this))
                {
                    PutIntoSlot(slot);
                    OnMovedToSlotEvent.Invoke();
                    return;
                }
            }
        }

        _currentSlot.PutInOriginalPosition();
        OnCancelMoveEvent.Invoke();
    }

    public void ReturnHome()
    {
        PutIntoSlot(_homeSlot);
        OnReturnedHomeEvent.Invoke();
    }

    private void PutIntoSlot(Slot slot)
    {
        _currentSlot?.Clear();
        _currentSlot = slot;
        slot.PutPieceIn(this);
        
        //TODO animation of movement
    }
}
