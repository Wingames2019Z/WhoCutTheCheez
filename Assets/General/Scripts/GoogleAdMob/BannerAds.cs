using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    private BannerView bannerView;
    ShopDataModel shopDataModel;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        shopDataModel = GameDataSystem.ShopDataLoad();
        Debug.Log("shopDataModel.NoAd" + shopDataModel.NoAd);
        Debug.Log(Application.persistentDataPath);
        if (shopDataModel.NoAd)
            return;
        this.RequestBanner();

    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2267873372430897/8096355865";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-2267873372430897/4596094557";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}
