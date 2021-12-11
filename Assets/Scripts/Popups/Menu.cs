using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private PortraitManager _portraits;
    [SerializeField] private GameObject _loadingPanel;

    public void Play()
    {
        SceneManager.LoadSceneAsync(TutorialSequence.ShouldShowTutorial() ? "TutorialScene" : "Game");
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
        
        //TODO additively load loading scene
        
        SceneManager.LoadSceneAsync("Loading");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
