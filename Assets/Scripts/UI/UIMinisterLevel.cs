using TMPro;
using UnityEngine;

public class UIMinisterLevel : MonoBehaviour
{
    [SerializeField] private Minister _minister;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _boredomNumberText;
    [SerializeField] private TMP_Text _boredomDescText;
    private int _currentLvl;
    private int _currentBoredom;

    private void Start()
    {
        _currentLvl = _minister.Level;
        _levelText.text = _currentLvl.ToString();
        
        _currentBoredom = _minister.Boredom;
        _boredomNumberText.text = _currentBoredom.ToString();
        _boredomDescText.text = BoredomText(_currentBoredom);
    }

    private void Update()
    {
        if (_minister.Level != _currentLvl)
        {
            _currentLvl = _minister.Level;
            _levelText.text = _currentLvl.ToString();
        }

        if (_minister.Boredom != _currentBoredom)
        {
            _currentBoredom = _minister.Boredom;
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
            return "Normal";
        else if (boredomValue > -10)
            return "Tired";
        else
            return "Exhausted";
    }
}
