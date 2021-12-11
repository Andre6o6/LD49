using UnityEngine;
using UnityEngine.UI;

public class UITutorialToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    
    private void OnEnable()
    {
        _toggle.isOn = TutorialSequence.ShouldShowTutorial();
        _toggle.onValueChanged.AddListener(ShowTutorial);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ShowTutorial);
    }

    private void ShowTutorial(bool value)
    {
        TutorialSequence.SetTutorialShown(!value);
    }
}
