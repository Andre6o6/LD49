using System.Threading.Tasks;
using UnityEngine;

public class TaskSounds : MonoBehaviour
{
    [SerializeField] private TaskCard _task;
    [SerializeField] private AudioEventChannel _channelSo;
    [SerializeField] private AudioClip[] _spawnClips;
    [SerializeField] private AudioClip[] _successClips;
    [SerializeField] private AudioClip[] _failClips;
    [SerializeField] private AudioClip[] _winPointSoundsClips;
    [SerializeField, Range(0.5f, 1.5f)] private float _minPitchRange, _maxPitchRange;
    [SerializeField] private bool _skipSpawnSound;

    private void Awake()
    {
        _task.OnTaskSuccess.AddListener(PlaySuccessSound);
        _task.OnTaskFailed.AddListener(PlayFailSound);
    }

    private void Start()
    {
        PlaySpawnSound();
    }

    private void PlaySpawnSound()
    {
        if (_skipSpawnSound || _task.GetData().SuperTask) return;
        
        //await Task.Delay(Random.Range(0, 8) * 25);
        
        var clip = _spawnClips[Random.Range(0, _spawnClips.Length)];
        float pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _channelSo.RaisePlayEvent(clip, AudioEventFilter.TaskCreated, volume:0.6f, pitch: pitch);
    }

    private void PlaySuccessSound()
    {
        if (_task.GetData().WinPoint)
        {
            var clip = _winPointSoundsClips[Random.Range(0, _winPointSoundsClips.Length)];
            _channelSo.RaisePlayEvent(clip);
        }
        else
        {
            var clip = _successClips[Random.Range(0, _successClips.Length)];
            _channelSo.RaisePlayEvent(clip, AudioEventFilter.TaskSuccess);
        }
    }
    
    private async void PlayFailSound()
    {
        await Task.Delay(150);
        
        var clip = _failClips[Random.Range(0, _failClips.Length)];
        float pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _channelSo.RaisePlayEvent(clip, AudioEventFilter.TaskFail, volume:0.6f, pitch: pitch);
    }
}
