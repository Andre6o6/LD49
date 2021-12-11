using UnityEngine;
using UnityEngine.UI;

public class UIMuteToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioManager _audio;
    
    private void Awake()
    {
        _toggle.isOn = GameSettings.MuteSounds;
        _toggle.onValueChanged.AddListener(_audio.SetMuteSounds);
    }
}
