using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMinisterLevel : MonoBehaviour
{
    [SerializeField] private Minister _minister;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _boredomNumberText;
    [SerializeField] private Image _backImage;
    [SerializeField] private Color _armyColor;
    [SerializeField] private Color _moneyColor;
    [SerializeField] private Color _moodColor;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private PortraitManager _portraits;
    private int _currentLvl;
    private int _currentBoredom;

    private void Start()
    {
        _currentLvl = _minister.Level;
        _levelText.text = _currentLvl.ToString();
        
        _currentBoredom = _minister.Boredom;
        //_boredomNumberText.text = _currentBoredom.ToString();

        SetColor();
        _portraitImage.sprite = _portraits.PortraitForMinister(_minister.Suite);
    }

    private void Update()
    {
        if (_minister.Level != _currentLvl)
        {
            _currentLvl = _minister.Level;
            _levelText.text = _currentLvl.ToString();
        }

        /*if (_minister.Boredom != _currentBoredom)
        {
            _currentBoredom = _minister.Boredom;
            _boredomNumberText.text = _currentBoredom.ToString();
        }*/
    }
    
    private void SetColor()
    {
        if (_minister.Suite == MinisterSuite.Army)
            _backImage.color = _armyColor;
        if (_minister.Suite == MinisterSuite.Money)
            _backImage.color = _moneyColor;
        if (_minister.Suite == MinisterSuite.Mood)
            _backImage.color = _moodColor;
    }
}
