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
        }
    }
}
