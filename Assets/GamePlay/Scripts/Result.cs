using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Result : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] GameObject RewardButton;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] RewardAds RewardAds;
    [SerializeField] InterstitialAds InterstitialAds;
    int RewardTime = 0;
    public void ScoreSet(int score)
    {
        ScoreText.text = score.ToString() + "pt";
        Invoke(nameof(Anime), 2f);
    }

    void Anime()
    {
        if(RewardTime > 0)
        {
            RewardButton.SetActive(false);
        }
        this.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);
    }
    public void Restart()
    {
        var ShopDataModel = GameDataSystem.ShopDataLoad();
        if (ShopDataModel.NoAd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            InterstitialAds.Show(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }

    }
    public void Continue()
    {
        RewardTime++;
        RewardAds.Show(EndAction);

        void EndAction()
        {
            this.transform.localScale = Vector3.zero;
            GameController.Continue();
        }
    }
    public void OurGamePressed()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/developer?id=wingames2019");
#elif UNITY_IPHONE
        Application.OpenURL("https://apps.apple.com/developer/katsuya-shirai/id1453314952");
#else
    Debug.Log("Any other platform");
#endif
    }
    public void RateUsPressed()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Wingames.InstantMemory");
#elif UNITY_IPHONE
        Application.OpenURL("https://apps.apple.com/app/instantmemory/id1453314953");
#else
    Debug.Log("Any other platform");
#endif
    }
}
