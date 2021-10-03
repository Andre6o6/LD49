using UnityEngine;
using UnityEngine.UI;

public class UIWinPoints : MonoBehaviour
{
    [SerializeField] private Image _imageTemplate;

    private void Awake()
    {
        EmpireController.Instance.OnWinPointsChangedEvent.AddListener(AddWinPoint);
    }

    private void AddWinPoint(int _)
    {
        var icon = Instantiate(_imageTemplate, this.transform);
        icon.gameObject.SetActive(true);
    }
}
