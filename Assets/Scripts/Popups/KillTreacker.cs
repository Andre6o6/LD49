using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillTreacker : Singleton<KillTreacker>
{
    [SerializeField] private GameObject _killPopup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _killerImage;
    [SerializeField] private Image _victimImage;
    [SerializeField] private PortraitManager _portraits;
    private string _format;
    
    private void Start()
    {
        _format = _text.text;
    }

    public void OnMinisterDied(Minister victim, Minister killer)
    {
        _killPopup.SetActive(true);
        _text.text = string.Format(_format, killer.GetPositionName(), victim.GetPositionName());
        _killerImage.sprite = _portraits.PortraitForMinister(killer.Suite);
        _victimImage.sprite = _portraits.PortraitForMinister(victim.Suite);
    }
}
