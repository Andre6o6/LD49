using System.Threading.Tasks;
using UnityEngine;

public class PieceSounds : MonoBehaviour
{
    [SerializeField] private DragablePiece _piece;
    [SerializeField] private AudioEventChannel _channelSo;
    [SerializeField] private AudioClip[] _moveClips;
    [SerializeField] private AudioClip[] _returnClips;
    [SerializeField] private AudioClip[] _upClips;
    [SerializeField, Range(0, 2)] private float _minPitchRange, _maxPitchRange;
    
    private void Awake()
    {
        _piece.OnBeginDragEvent.AddListener(PlayUpSound);
        _piece.OnMovedToSlotEvent.AddListener(PlayMoveSound);
        _piece.OnCancelMoveEvent.AddListener(PlayReturnSound);
        _piece.OnReturnedHomeEvent.AddListener(PlayReturnHomeSound);
    }

    private void PlayMoveSound()
    {
        var clip = _moveClips[Random.Range(0, _moveClips.Length)];
        float pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _channelSo.RaisePlayEvent(clip, pitch: pitch);
    }
    
    private void PlayUpSound()
    {
        var clip = _upClips[Random.Range(0, _upClips.Length)];
        _channelSo.RaisePlayEvent(clip);
    }
    
    private void PlayReturnSound()
    {
        var clip = _returnClips[Random.Range(0, _returnClips.Length)];
        _channelSo.RaisePlayEvent(clip);
    }
    
    private async void PlayReturnHomeSound()
    {
        await Task.Delay(100);
        
        var clip = _returnClips[Random.Range(0, _returnClips.Length)];
        _channelSo.RaisePlayEvent(clip);
    }

}
