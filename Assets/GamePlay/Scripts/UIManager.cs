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
    [SerializeField] GameController GameController;

    [SerializeField] Image Gage;
    UserDataModel UserDataModel;
    
    Vector3 ScoreScale;
    float ScoreScaleAmount = 0.2f;
    int Level;
    Tween tween;
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
        ScoreText.text =  score + "pt";
        ScoreAnime();
        TextSizeSet();
    }
    void ScoreAnime()
    {
        tween.Kill();
        ScoreText.transform.localScale = Vector3.one;
        tween = ScoreText.transform.DOPunchScale(ScoreScale, 0.2f);
    }



    void TextSizeSet()
    {
        var level = GameController.GetLevel();

        if (Level == level)
            return;
        Level = level;
        switch (Level)
        {
            case 0:
                ScoreScaleAmount = 0.2f;
                ScoreText.fontSize = 65;
                break;
            case 1:
                ScoreScaleAmount = 0.3f;
                ScoreText.fontSize = 70;
                break;
            case 2:
                ScoreScaleAmount = 0.4f;
                ScoreText.fontSize = 80;
                break;
            case 3:
                ScoreScaleAmount = 0.5f;
                ScoreText.fontSize = 90;
                break;
            case 4:
                ScoreScaleAmount = 0.6f;
                ScoreText.fontSize = 100;
                break;
            case 5:
                ScoreScaleAmount = 0.7f;
                ScoreText.fontSize = 110;
                break;
            default:
                ScoreScaleAmount = 0.2f;
                ScoreText.fontSize = 65;
                break;
        }
        ScoreScale = new Vector3(ScoreScaleAmount, ScoreScaleAmount, ScoreScaleAmount);
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
        if(Gage.fillAmount >= 0 && Gage.fillAmount < 0.6f)
        {
            Gage.color = Color.green;
        }
        else if (Gage.fillAmount >= 0.6f && Gage.fillAmount < 0.85f)
        {
            Gage.color = Color.yellow;
        }
        else if (Gage.fillAmount >= 0.85f)
        {
            Gage.color = Color.red;
        }
    }
}
