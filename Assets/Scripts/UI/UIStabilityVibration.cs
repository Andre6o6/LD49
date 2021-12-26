using System.Collections;
using UnityEngine;

public class UIStabilityVibration : MonoBehaviour
{
    private static bool _frameFlag;    //One vibration call per frame
    private static bool _frameFlagLose;
    
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
        if (delta >= 0) return;
        
        if (value == 0 && !_frameFlagLose)
        {
            _frameFlagLose = true;
            StartCoroutine(VibrateCoroutine(true));
        }

        if (value > 0 && !_frameFlag) StartCoroutine(VibrateCoroutine());
    }

    private IEnumerator VibrateCoroutine(bool lose=false)
    {
        _frameFlag = true;

        if (lose) yield return new WaitForSeconds(0.001f * _msTimeOnHit + 0.1f);
        
        int length = lose ? _msTimeOnDeath : _msTimeOnHit;
        Vibration.Vibrate(length);
        
        yield return null;
        _frameFlag = false;
        _frameFlagLose = false;
    }
}
