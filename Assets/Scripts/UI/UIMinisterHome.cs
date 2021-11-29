using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMinisterHome : MonoBehaviour
{
    [SerializeField] private Minister _minister;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private string _levelFormat = "Level: {0}";
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _boredomNumberText;
    [SerializeField] private TMP_Text _boredomDescText;
    [SerializeField] private Image _backCardImage;
    [SerializeField] private MinisterColors _colors;
    [Header("Death")]
    [SerializeField] private Image _bloodImage;
    [SerializeField] private TMP_Text _deadText;

    private void Awake()
    {
        _minister.OnMinisterBoredomChangeEvent += OnMinisterBoredomChanged;
        _minister.OnMinisterLevelUpEvent += OnMinisterLevelUp;
        _minister.OnMinisterDiedEvent += OnMinisterDead;
    }

    private void Start()
    {
        OnMinisterLevelUp(_minister.Level);
        OnMinisterBoredomChanged(_minister.Boredom);
        _backCardImage.color = _colors.GetColor(_minister.Suite);
        _nameText.text = _minister.GetPositionName();
    }

    private void OnMinisterLevelUp(int level)
    {
        _levelText.text = string.Format(_levelFormat, level);
    }

    private void OnMinisterBoredomChanged(int boredom)
    {
        if (_boredomNumberText != null)
            _boredomNumberText.text = boredom.ToString();
        _boredomDescText.text = _minister.GetBoredomText();
    }

    private void OnMinisterDead()
    {
        _bloodImage.gameObject.SetActive(true);
        _deadText.gameObject.SetActive(true);
        this.enabled = false;
    }
}
