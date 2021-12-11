using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FadeGameSounds : MonoBehaviour
{
    private const string MixerGameVolumeParam = "GameVolume";
    
    [SerializeField] private AudioMixer _mixer;
    [SerializeField, Range(-80, 0)] private float _volumeToFade = -20;
    [SerializeField] private float _time;
    private float _defaultVolume;
    private Coroutine _fadeCoroutine;
    
    private void OnEnable()
    {
        if (GameSettings.MuteSounds) return;
        
        _mixer.GetFloat(MixerGameVolumeParam, out _defaultVolume);
        
        if (_time <= 0)
            Fade();
        else
            FadeForTime();
    }
    
    private void OnDisable()
    {
        if (GameSettings.MuteSounds) return;
        
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _mixer.SetFloat(MixerGameVolumeParam, _defaultVolume);
    }

    private void Fade()
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeFromToCoroutine(_volumeToFade, _defaultVolume, 1f));
    }
    
    private void FadeForTime()
    {
        if (_fadeCoroutine == null)
            _fadeCoroutine = StartCoroutine(FadeForTimeCoroutine());
    }

    private IEnumerator FadeFromToCoroutine(float to, float from, float time)
    {
        float fadeTime = time;
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            var volume = Mathf.Lerp(to, from, fadeTime / time);
            _mixer.SetFloat(MixerGameVolumeParam, volume);
            yield return null;
        }
    }

    private IEnumerator FadeForTimeCoroutine()
    {
        yield return FadeFromToCoroutine(_volumeToFade, _defaultVolume, 1f);
        
        yield return new WaitForSeconds(_time);
        
        //yield return FadeFromToCoroutine(_defaultVolume, _volumeToFade, 0.5f);

        _mixer.SetFloat(MixerGameVolumeParam, _defaultVolume);
        _fadeCoroutine = null;
    }
}
