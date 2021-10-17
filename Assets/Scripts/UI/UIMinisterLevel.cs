using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMinisterLevel : MonoBehaviour
{
    [SerializeField] private Minister _minister;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _backImage;
    [SerializeField] private MinisterColors _colors;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private PortraitManager _portraits;

    private void Awake()
    {
        _minister.OnMinisterLevelUpEvent += OnLevelUp;
    }

    private void Start()
    {
        SetLevelText(_minister.Level);

        _backImage.color = _colors.GetColor(_minister.Suite);
        _portraitImage.sprite = _portraits.PortraitForMinister(_minister.Suite);
    }

    private void SetLevelText(int level)
    {
        _levelText.text = level.ToString();
    }

    private void OnLevelUp(int level)
    {
        SetLevelText(level);
        
        //Anim
        var seq = DOTween.Sequence();
        seq.Append(_levelText.transform.DOScale(1.5f * Vector3.one, 0.2f))
            .Append(_levelText.transform.DOScale(Vector3.one, 0.2f));
    }
}
