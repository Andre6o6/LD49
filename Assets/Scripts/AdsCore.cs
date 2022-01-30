using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsCore : MonoBehaviour
{
    public static bool BannerShown { get; private set; }

    private static bool _isInitialized;
    private static string _gameId = "4500891";
    private static string _banner = "Banner_Android";

    [SerializeField] private bool _testMode = true;
    [SerializeField] private TMP_Text _text;
    
    public void Awake()
    {
        if (_isInitialized)
        {
            _text.text = "Ad: ON";
            return;
        }
        
        Advertisement.Initialize(_gameId, _testMode);
        _isInitialized = true;
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Debug.LogError($"ADS initialized {Advertisement.isShowing}, {Advertisement.isSupported}");
        _text.text = "Ad: ON";
    }

    public static void ShowBanner()
    {
        if (!_isInitialized) return;
        
        Advertisement.Banner.Show(_banner);
        BannerShown = true;
        Debug.LogError($"Show banner, {Advertisement.Banner.isLoaded}; (inited) {Advertisement.isInitialized}");
    }

    public static void HideBanner()
    {
        if (!_isInitialized) return;
        
        Advertisement.Banner.Hide();
        BannerShown = false;
        Debug.LogError("Hide banner");
    }
}
