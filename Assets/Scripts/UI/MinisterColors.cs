using UnityEngine;

[System.Serializable]
public class MinisterColors
{
    [SerializeField] private Color _armyColor;
    [SerializeField] private Color _moneyColor;
    [SerializeField] private Color _moodColor;
    [SerializeField] private Color _defaultColor = Color.black;
    
    public Color GetColor(MinisterSuite suite)
    {
        return suite switch
        {
            MinisterSuite.Army => _armyColor,
            MinisterSuite.Money => _moneyColor,
            MinisterSuite.Mood => _moodColor,
            _ => _defaultColor
        };
    }
}
