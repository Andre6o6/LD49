using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Color _armyColor;
    [SerializeField] private Color _moneyColor;
    [SerializeField] private Color _moodColor;
    [SerializeField] private TMP_Text _deadText;
    private int _currentLvl;
    private int _currentBoredom;
    
    private void Start()
    {
        _currentLvl = _minister.Level;
        _levelText.text = string.Format(_levelFormat, _currentLvl);
        
        _currentBoredom = _minister.Boredom;
        if (_boredomNumberText != null)
            _boredomNumberText.text = _currentBoredom.ToString();
        _boredomDescText.text = BoredomText(_currentBoredom);

        SetColor();
        _nameText.text = ChoosePositionName();
    }

    private void Update()
    {
        if (_minister.Dead)
        {
            _deadText.gameObject.SetActive(true);
            this.enabled = false;
            return;
        }

        if (_minister.Level != _currentLvl)
        {
            _currentLvl = _minister.Level;
            _levelText.text = string.Format(_levelFormat, _currentLvl);
        }

        if (_minister.Boredom != _currentBoredom)
        {
            _currentBoredom = _minister.Boredom;
            if (_boredomNumberText != null)
                _boredomNumberText.text = _currentBoredom.ToString();
            _boredomDescText.text = BoredomText(_currentBoredom);
        }
    }

    private string BoredomText(int boredomValue)
    {
        if (boredomValue >= 10)
            return "Bored to death";
        else if (boredomValue > 5)
            return "Bored";
        else if (boredomValue >= -5)
            return "Rested";
        else if (boredomValue > -10)
            return "Tired";
        else
            return "Exhausted";
    }

    private void SetColor()
    {
        if (_minister.Suite == MinisterSuite.Army)
            _backCardImage.color = _armyColor;
        if (_minister.Suite == MinisterSuite.Money)
            _backCardImage.color = _moneyColor;
        if (_minister.Suite == MinisterSuite.Mood)
            _backCardImage.color = _moodColor;
    }
    
    private string ChoosePositionName()
    {
        if (_minister.Suite == MinisterSuite.Army)
            return "Marshal";
        if (_minister.Suite == MinisterSuite.Money)
            return "Steward";
        if (_minister.Suite == MinisterSuite.Mood)
            return "Chancellor";

        return "";
    }
}
