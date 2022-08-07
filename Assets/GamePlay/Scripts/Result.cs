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
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] RewardAds RewardAds;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ScoreSet(int score)
    {
        ScoreText.text = score.ToString() + "pt";
        Invoke(nameof(Anime), 2f);
    }

    void Anime()
    {
        this.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
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
