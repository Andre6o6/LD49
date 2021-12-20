using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVanish : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField, Min(0)] private float _minTime = 0.5f;
    [SerializeField, Min(0)] private float _maxTime = 1f;
    private bool _busy;

    private void Awake()
    {
        EmpireController.Instance.OnGameWonEvent.AddListener(SetTransparent);
    }

    public void SetTransparent()
    {
        if (!_busy)
            StartCoroutine(SetTransparentCoroutine());
    }

    private IEnumerator SetTransparentCoroutine()
    {
        _busy = true;
        float time = Random.Range(_minTime, _maxTime);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float alpha = 1 - t / time;
            foreach (var image in _images)
            {
                Color c = image.color;
                c.a = alpha;
                image.color = c;
            }
            
            foreach (var text in _texts)
            {
                Color c = text.color;
                c.a = alpha;
                text.color = c;
            }

            yield return null;
        }
        
        foreach (var image in _images)
        {
            image.enabled = false;
        }
            
        foreach (var text in _texts)
        {
            text.enabled = false;
        }
        _busy = false;
    }
}
