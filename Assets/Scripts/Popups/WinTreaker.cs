using DG.Tweening;
using UnityEngine;

public class WinTreaker : MonoBehaviour
{
    [SerializeField] private GameObject _winPopup;
    
    private void Awake()
    {
        EmpireController.Instance.OnWinPointsChangedEvent.AddListener(OnWinPoint);
    }

    private void OnWinPoint(int value)
    {
        if (value >= 3)
        {
            _winPopup.SetActive(true);
            _winPopup.transform.localScale = Vector3.zero;
            _winPopup.transform.DOScale(Vector3.one, 0.75f);
        }
    }
}
