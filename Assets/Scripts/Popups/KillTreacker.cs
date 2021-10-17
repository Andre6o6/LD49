using TMPro;
using UnityEngine;

public class KillTreacker : Singleton<KillTreacker>
{
    [SerializeField] private GameObject _killPopup;
    [SerializeField] private TMP_Text _text;
    private string _format;
    
    private void Start()
    {
        _format = _text.text;
    }

    public void OnMinisterDied(Minister victim, Minister killer)
    {
        _killPopup.SetActive(true);
        _text.text = string.Format(_format, killer.GetPositionName(), victim.GetPositionName());
    }
}
