using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Event Channel")]
public class AudioEventChannel : ScriptableObject
{
    public delegate void AudioPlayHandler(AudioClip audioClip, float volume, float pitch);
    public event AudioPlayHandler OnAudioPlayRequested;
    
    public delegate void AudioFilteredPlayHandler(AudioClip audioClip, AudioEventFilter filter, float volume, float pitch);
    public event AudioFilteredPlayHandler OnAudioUniquePlayRequested;
    
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
    
    public void RaisePlayEvent(AudioClip audioClip, float volume = 1, float pitch = 1)
    {
        OnAudioPlayRequested?.Invoke(audioClip, volume, pitch);
    }
    
    public void RaisePlayEvent(AudioClip audioClip, AudioEventFilter filter, float volume = 1, float pitch = 1)
    {
        OnAudioUniquePlayRequested?.Invoke(audioClip, filter, volume, pitch);
    }
}

public enum AudioEventFilter
{
    TaskCreated,
    TaskFail,
    TaskSuccess,
}
