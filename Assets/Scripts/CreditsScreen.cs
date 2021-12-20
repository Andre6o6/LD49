using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField] private Image _titleFade;
    [SerializeField] private RectTransform _title;
    [SerializeField] private TMP_Text[] _texts1;
    [SerializeField] private TMP_Text[] _texts2;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private float _timeBefore = 4f;
    
    private IEnumerator Start()
    {
        foreach (var text in _texts1) text.enabled = false;
        foreach (var text in _texts2) text.enabled = false;
        _buttonNext.gameObject.SetActive(false);

        var pos = _title.anchoredPosition;
        _title.anchoredPosition = Vector2.zero;

        _titleFade.DOFade(1, 1f).From();
        
        yield return new WaitForSeconds(_timeBefore);

        _title.DOAnchorPos(pos, 2f);
        
        yield return new WaitForSeconds(3.5f);
        
        _texts1[0].enabled = true;
        _texts1[0].DOFade(0, 1f).From();
        
        yield return new WaitForSeconds(3f);
        
        _texts1[1].enabled = true;
        _texts1[1].DOFade(0, 1f).From();
        
        yield return new WaitForSeconds(3f);
        
        foreach (var text in _texts2)
        {
            text.enabled = true;
            text.DOFade(0, 1f).From();
        }
        
        yield return new WaitForSeconds(4f);
        
        _buttonNext.gameObject.SetActive(true);
    }
}
