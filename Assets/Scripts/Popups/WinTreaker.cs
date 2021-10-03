using System.Collections;
using System.Collections.Generic;
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
        }
    }
}
