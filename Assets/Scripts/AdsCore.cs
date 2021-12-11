using UnityEngine;
using UnityEngine.Advertisements;

public class AdsCore : MonoBehaviour
{
    private static bool _isInitialized;
    private static string _gameId = "4500891";
    private static string _banner = "Banner_Android";

    [SerializeField] private bool _testMode = true;
    
    public void Awake()
    {
        if (_isInitialized) return;
     
        //TODO handle platforms
        //if (!Application.isMobilePlatform) return;
        
        Advertisement.Initialize(_gameId, _testMode);
        _isInitialized = true;
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public static void ShowBanner()
    {
        if (!_isInitialized) return;
        
        Advertisement.Banner.Show(_banner);
    }

    public static void HideBanner()
    {
        if (!_isInitialized) return;
        
        Advertisement.Banner.Hide();
    }
}
