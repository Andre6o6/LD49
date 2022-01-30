using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private const string MixerGameVolumeParam = "GameVolume";
    private const string MixerEventVolumeParam = "EventVolume";
    
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioEventChannel _channelSo;
    [SerializeField] private SoundEmitter _soundEmitterPrefab;
    [SerializeField, Min(0)] private int _prewarmCount = 5;

    private readonly List<AudioEventFilter> _uniqueEventsBuffer = new List<AudioEventFilter>();
    
    private void Awake()
    {
        if (_channelSo != null)
        {
            _channelSo.OnAudioPlayRequested += PlaySound;
            _channelSo.OnAudioUniquePlayRequested += PlayUniqueSound;
        }

        if (GameSettings.MuteSounds) SetMuteSounds(true);
    }

    private void OnDestroy()
    {
        if (_channelSo != null)
        {
            _channelSo.OnAudioPlayRequested -= PlaySound;
            _channelSo.OnAudioUniquePlayRequested -= PlayUniqueSound;
        }
    }

    private void PlaySound(AudioClip audioClip, float volume, float pitch)
    {
        SoundEmitter soundEmitter = Instantiate(_soundEmitterPrefab, this.transform);
        soundEmitter.PlayAudioClip(audioClip, volume, pitch);
        
        soundEmitter.OnSoundFinishedPlaying += OnSoundFinishedPlaying;
    }

    //TODO time offset
    private async void PlayUniqueSound(AudioClip audioClip, AudioEventFilter filter, float volume, float pitch)
    {
        if (_uniqueEventsBuffer.Contains(filter)) return;
        
        _uniqueEventsBuffer.Add(filter);

        await Task.Yield();    //TODO delay for audioClip.Length
        
        PlaySound(audioClip, volume, pitch);
        _uniqueEventsBuffer.Remove(filter);
    }

    private void OnSoundFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.Stop();

        Destroy(soundEmitter.gameObject);
    }

    public void SetMuteSounds(bool mute)
    {
        GameSettings.MuteSounds = mute;    //For toggle value

        float volume = mute ? -80 : 0;

        _mixer.SetFloat(MixerGameVolumeParam, volume);
        _mixer.SetFloat(MixerEventVolumeParam, volume);
    }
}
