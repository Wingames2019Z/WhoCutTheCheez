using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BestScoreText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI ReleaseGasText;
    [SerializeField] Image Gage;
    UserDataModel UserDataModel;
    
    Vector3 ScoreScale;
    float ScoreScaleAmount = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        ScoreScale = new Vector3(ScoreScaleAmount, ScoreScaleAmount, ScoreScaleAmount);
        UserDataModel = GameDataSystem.UserDataLoad();
        BestScoreSet(UserDataModel.BestScore);

    }
    public bool GetReleaseGasTextActive() => ReleaseGasText.gameObject.activeSelf;
    private void Update()
    {
        if (!ReleaseGasText.gameObject.activeSelf)
            return;
    }

    // Update is called once per frame
    public void BestScoreSet(int bestScore)
    {
        BestScoreText.text = "BEST:" + bestScore + "pt";
    }
    public void ScoreSet(int score)
    {
        ScoreText.text = score + "pt";
        ScoreText.transform.DOPunchScale(ScoreScale, 0.2f);
    }
    public void ReleaseGasTextAnime()
    {
        ReleaseGasText.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        
    }
    public void StopReleaseGasTextAnime()
    {
        ReleaseGasText.gameObject.SetActive(false);
    }
    public void GageSet(float amount)
    {
        Gage.fillAmount = amount;
    }

}
