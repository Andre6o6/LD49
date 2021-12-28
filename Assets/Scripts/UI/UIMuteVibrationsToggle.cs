using UnityEngine;
using UnityEngine.UI;

public class UIMuteVibrationsToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    
    private void Awake()
    {
        _toggle.isOn = GameSettings.MuteSounds;
        _toggle.onValueChanged.AddListener(SetVibrationsMuted);
    }

    private void SetVibrationsMuted(bool value)
    {
        Vibration.SetActive(!value);
    }
}
