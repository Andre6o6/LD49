using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ChanceResultPopup : MonoBehaviour
{
    [SerializeField] private TaskCard _card;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _successFormat = "{0}% SUCCESS";
    [SerializeField] private string _failFormat = "{0}% FAIL";
    [SerializeField] private Color _successColor = Color.green;
    [SerializeField] private Color _failColor = Color.red;
    [SerializeField] private float _lifetime;
    [SerializeField] private Vector2 _movement;
    
    private void Start()
    {
        _card.OnChanceFailEvent += SetFailText;
        _card.OnChanceSuccessEvent += SetSuccessText;
        _text.enabled = false;
    }

    private void SetSuccessText(float chance)
    {
        Reparent();
        _text.enabled = true;
        
        int percent = (int) (chance * 100);
        _text.text = string.Format(_successFormat, percent);
        _text.color = _successColor;
        Animate();
    }
    
    private void SetFailText(float chance)
    {
        Reparent();
        _text.enabled = true;
        
        int percent = (int) (chance * 100);
        _text.text = string.Format(_failFormat, percent);
        _text.color = _failColor;
        Animate();
    }

    private void Reparent()
    {
        transform.SetParent(transform.parent.parent.parent);
        transform.SetAsLastSibling();
    }

    private void Animate()
    {
        var fadeColor = _text.color;
        fadeColor.a = 0;
        
        Sequence seq = DOTween.Sequence();
        seq.Append(_text.transform.DOMove(transform.position + (Vector3) _movement, _lifetime)
                .SetEase(Ease.InQuad));
        seq.Append(_text.DOColor(fadeColor, 0.2f)
            .OnComplete(() => Destroy(this.gameObject)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,_movement);
    }
}
