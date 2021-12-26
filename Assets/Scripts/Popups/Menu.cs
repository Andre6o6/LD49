using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private PortraitManager _portraits;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _fadePanel;

    public void Play()
    {
        SceneManager.LoadSceneAsync(TutorialSequence.ShouldShowTutorial() ? "TutorialScene" : "Game");
    }
    
    public void ToCredits()
    {
        if (_fadePanel == null)
        {
            SceneManager.LoadSceneAsync("Credits");
            return;
        }

        var seq = DOTween.Sequence();
        seq.AppendCallback(() => _fadePanel.gameObject.SetActive(true));
        seq.Append(_fadePanel.DOFade(1, 1f));
        seq.AppendCallback(() => SceneManager.LoadSceneAsync("Credits"));
    }
    
    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void Retry()
    {
        _portraits.CachePortraits();
        if (_loadingPanel != null) 
            _loadingPanel.SetActive(true);
        
        AdsCore.ShowBanner();    //Show banner early for it to init on time
        SceneManager.LoadSceneAsync("Loading");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
