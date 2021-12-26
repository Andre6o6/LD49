using System.Collections;
using UnityEngine;

public class UIStabilityVibration : MonoBehaviour
{
    private static bool _frameFlag;    //One vibration call per frame
    
    [SerializeField] private EmpireController _empire;
    [SerializeField, Min(0)] private int _msTimeOnHit = 100;
    [SerializeField, Min(0)] private int _msTimeOnDeath = 500;

    private void Awake()
    {
        _empire ??= EmpireController.Instance;
        _empire.OnStabilityChangedEvent.AddListener(VibrateOnHit);
    }

    private void VibrateOnHit(int value, int delta)
    {
        if (_frameFlag || delta >= 0) return;

        int length = value > 0 ? _msTimeOnHit : _msTimeOnDeath;
        StartCoroutine(VibrateCoroutine(length));
    }

    private IEnumerator VibrateCoroutine(int length)
    {
        _frameFlag = true;
        Vibration.Vibrate(length);
        
        yield return null;
        _frameFlag = false;
    }
}
