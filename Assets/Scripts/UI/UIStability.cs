using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStability : MonoBehaviour
{
    [SerializeField] private EmpireController _empire;
    [SerializeField] private TMP_Text _stabilityText;
    [SerializeField] private Slider _stabilitySlider;
    
    private void Awake()
    {
        _stabilityText.text = _empire.MaxStability.ToString();
        _stabilitySlider.maxValue = _empire.MaxStability;
        _stabilitySlider.value = _empire.MaxStability;
        _empire.OnStabilityChangedEvent.AddListener(OnStabilityChange);
    }

    private void OnStabilityChange(int value, int delta)
    {
        _stabilityText.text = value.ToString();
        _stabilitySlider.value = value;
    }
}
