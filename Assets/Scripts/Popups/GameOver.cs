using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _losePopup;
    
    private void Awake()
    {
         EmpireController.Instance.OnStabilityChangedEvent.AddListener(OnStabilityFall);
    }

    private void OnStabilityFall(int value)
    {
        if (value <= 0)
        {
            _losePopup.SetActive(true);
            _losePopup.transform.localScale = Vector3.zero;
            _losePopup.transform.DOScale(Vector3.one, 0.75f);
        }
    }
}
