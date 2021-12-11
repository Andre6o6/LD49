using System.Collections;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public event System.Action<SoundEmitter> OnSoundFinishedPlaying;
    
    private void Awake()
    {
        _audioSource ??= this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }
    
    public void PlayAudioClip(AudioClip clip, float volume, float pitch)
    {
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.pitch = pitch;

        _audioSource.Play();
        StartCoroutine(FinishedPlaying(clip.length));
    }
    
    public void Stop()
    {
        _audioSource.Stop();
    }
    
    private IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);    //TODO Fixme
        OnSoundFinishedPlaying?.Invoke(this); // The AudioManager will pick this up
    }
}
