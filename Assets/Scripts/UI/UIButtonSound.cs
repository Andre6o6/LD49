using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null) _source = this.gameObject.AddComponent<AudioSource>();
        
        _source.playOnAwake = false;
        _source.loop = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_audioClip);
    }
}
