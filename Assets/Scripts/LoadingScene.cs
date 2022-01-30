using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private float _loadingTime = 5;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _finishImage;
    
    private IEnumerator Start()
    {
        AdsCore.ShowBanner();
        
        for (float time = 0; time < _loadingTime; time += Time.deltaTime)
        {
            yield return null;
            _progressBar.fillAmount = time / _loadingTime;
        }
        _progressBar.fillAmount = 1;
        
        if (_finishImage != null)
            _finishImage.color = Color.white;

        yield return null;

        AdsCore.HideBanner();
        
        SceneManager.LoadSceneAsync(TutorialSequence.ShouldShowTutorial() ? "TutorialScene" : "Game");
    }
}
