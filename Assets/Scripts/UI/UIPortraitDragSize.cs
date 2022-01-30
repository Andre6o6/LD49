using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPortraitDragSize : MonoBehaviour
{
    [SerializeField] private DragablePiece _piece;
    [SerializeField] private RectTransform _portraitRectTransform;
    [SerializeField] private Vector2 _fullPortraitSize = new Vector2(160, 160);
    [SerializeField] private Vector2 _dragPortraitSize = new Vector2(110, 110);
    
    private void Awake()
    {
        _piece ??= GetComponent<DragablePiece>();
        _portraitRectTransform ??= GetComponent<RectTransform>();
        
        _piece.OnBeginDragEvent.AddListener(MinimizePortrait);
        _piece.OnMovedToSlotEvent.AddListener(ChangePortraitSize);
        _piece.OnCancelMoveEvent.AddListener(ChangePortraitSize);
        _piece.OnReturnedHomeEvent.AddListener(ChangePortraitSize);    //can be called externally
    }

    private void ChangePortraitSize()
    {
        _portraitRectTransform.sizeDelta = _piece.IsHome ? _fullPortraitSize : _dragPortraitSize;
    }
    private void ChangePortraitSize(bool notHome) => ChangePortraitSize();

    private void MinimizePortrait()
    {
        _portraitRectTransform.sizeDelta = _dragPortraitSize;
    }
}
