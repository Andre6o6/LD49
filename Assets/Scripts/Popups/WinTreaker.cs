using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WinTreaker : MonoBehaviour
{
    [SerializeField] private GameObject _winPopup;
    private Coroutine _winCoroutine;
    
    private void Awake()
    {
        EmpireController.Instance.OnWinPointsChangedEvent.AddListener(OnWinPoint);
    }

    private void OnWinPoint(int value)
    {
        if (value >= 3)
        {
            if (_winCoroutine == null)
                _winCoroutine = StartCoroutine(WinScreenCoroutine());
        }
    }
    
    private IEnumerator WinScreenCoroutine()
    {
        DragablePiece.CanDrag = false;
        
        yield return new WaitForSeconds(0.5f);
        
        _winPopup.SetActive(true);
        _winPopup.transform.localScale = Vector3.zero;
        
        yield return new WaitForSeconds(0.5f);
        
        _winPopup.transform.DOScale(Vector3.one, 0.75f);
    }
}
