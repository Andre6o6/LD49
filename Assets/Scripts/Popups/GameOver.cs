using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _losePopup;
    private Coroutine _loseCoroutine;
    
    private void Awake()
    {
         EmpireController.Instance.OnStabilityChangedEvent.AddListener(OnStabilityFall);
    }

    private void OnStabilityFall(int value, int delta)
    {
        if (value <= 0)
        {
            if (_loseCoroutine == null)
                _loseCoroutine = StartCoroutine(LoseScreenCoroutine());
        }
    }

    private IEnumerator LoseScreenCoroutine()
    {
        DragablePiece.CanDrag = false;
        
        yield return new WaitForSeconds(0.5f);
        
        _losePopup.SetActive(true);
        _losePopup.transform.localScale = Vector3.zero;
        
        yield return new WaitForSeconds(0.5f);
        
        _losePopup.transform.DOScale(Vector3.one, 0.75f);
    }
}
