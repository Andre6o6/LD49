using TMPro;
using UnityEngine;

public class UIStability : MonoBehaviour
{
    [SerializeField] private EmpireController _empire;
    [SerializeField] private TMP_Text _stabilityText;
    
    private void Awake()
    {
        _empire.OnStabilityChangedEvent.AddListener(OnStabilityChange);
    }

    private void OnStabilityChange(int value)
    {
        _stabilityText.text = value.ToString();
    }
}
