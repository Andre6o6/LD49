using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip _onPointerAudioClip;
    [SerializeField] private AudioClip _onClickAudioClip;
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        if (_source == null)_source = GetComponent<AudioSource>();
        if (_source == null) _source = this.gameObject.AddComponent<AudioSource>();
        
        _source.playOnAwake = false;
        _source.loop = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_onPointerAudioClip != null)
            _source.PlayOneShot(_onPointerAudioClip);
    }

    public void OnClick()
    {
        if (_onClickAudioClip != null)
            _source.PlayOneShot(_onClickAudioClip);
    }
}
