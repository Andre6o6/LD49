using UnityEngine;

public class Ads : MonoBehaviour
{
    private void Awake()
    {
        AdsCore.ShowBanner();
    }

    private void OnDestroy()
    {
        AdsCore.HideBanner();
    }
}
