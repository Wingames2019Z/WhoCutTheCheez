using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GasSoundManager GasSoundManager;
    [SerializeField] EffectManager EffectManager;
    [SerializeField] UIManager UIManager;
    [SerializeField] Animator MainCharaAnimetor;
    [SerializeField] int Point;
    [SerializeField] GameObject InitialUI;
    [SerializeField] GameObject PlayingUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] Result Result;
    [SerializeField] float GasAmount;
    UserDataModel UserDataModel;
    bool IsPlaying = false;
    bool IsReleasing = false;
    float ReleasingTime = 0f;
    float PointTime = 0f;

    //GameConfig
    //-AddGas
    private readonly float InitialGasAmount = 50f;
    private readonly float MaxGasAmount = 100f;
    private readonly float AddGasAmount = 1f;
    //-ReleaseGas
    private readonly float ReleaseGasAmount = 1f;
    //-Point
    private float AddPointTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        UserDataModel = GameDataSystem.UserDataLoad();
        PlayingUI.SetActive(false);
        PointTime = AddPointTime;
        GasAmount = InitialGasAmount;
    }

    public bool GetIsPlaying() => IsPlaying;
    public bool GetIsReleasing() => IsReleasing;
    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying)
            return;
        if (Input.GetMouseButtonDown(0))
        {

            IsReleasing = true;
            MainCharaAnimetor.SetBool("IsRelease", true);
            if (!UIManager.GetReleaseGasTextActive())
                return;
            UIManager.StopReleaseGasTextAnime();
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsReleasing = false;
            MainCharaAnimetor.SetBool("IsRelease", false);
            ReleasingTime = 0f;
            PointTime = AddPointTime;
        }
        AddGas();
        ReleaseGas();
        PointCheck();
        GasGageSet();
        //Check Game Over
        if (GasAmount > MaxGasAmount && IsPlaying)
        {
            SetGameOver();
        }
    }
    void AddGas()
    {
        if (!IsReleasing)
        {
            GasAmount += AddGasAmount * Time.deltaTime;
        }
    }
    void ReleaseGas()
    {
        if (IsReleasing)
        {
            GasAmount -= ReleaseGasAmount * Time.deltaTime;
            if(GasAmount < 0)
            {
                GasAmount = 0;
            }
            ReleasingTime += Time.deltaTime;
        }
    }
    void PointCheck()
    {
        if(ReleasingTime >= PointTime)
        {
            
            var addPoint = AddPointGet();
            Point += addPoint;
            EffectManager.InstantiatePointEffect(addPoint);
            EffectManager.InstantiateGas();
            UIManager.ScoreSet(Point);

            if (UserDataModel.BestScore < Point)
            {
                UserDataModel.BestScore = Point;
                UIManager.BestScoreSet(UserDataModel.BestScore);
            }
            PointTime += AddPointTime;
        }
    }
    int AddPointGet()
    {
        var addPoint =(int)Mathf.Floor(ReleasingTime);
        if(addPoint < 1)
        {
            addPoint = 1;
        }
        return addPoint;
    }
    void GasGageSet()
    {
        UIManager.GageSet(GasAmount/MaxGasAmount);
    }
    public void StartButtonPressed()
    {
        InitialUI.SetActive(false);
        PlayingUI.SetActive(true);
        IsPlaying = true;
        UIManager.ReleaseGasTextAnime();
    }
    public void SetGameOver()
    {
        IsPlaying = false;
        IsReleasing = false;
        GameOverUI.SetActive(true);

        GasSoundManager.GameOverSound();
        EffectManager.InstantiateGas();
        Result.ScoreSet(Point);
        GameDataSystem.UserDataSave(UserDataModel);

        Invoke(nameof(SetAnime), 0.8f);
    }
    void SetAnime()
    {
        MainCharaAnimetor.SetBool("GameOver", true);
    }
    public void Continue()
    {
        MainCharaAnimetor.SetBool("GameOver", false);
        MainCharaAnimetor.Play("Blink");
        MainCharaAnimetor.Play("SittingIdle");
        var objects = GameObject.FindGameObjectsWithTag("Pedestrian");
        foreach (var item in objects)
        {
            Destroy(item);
        }

        var gasObjects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (var item in gasObjects)
        {
            Destroy(item);
        }
        PointTime = AddPointTime;
        GasAmount = InitialGasAmount;
        IsPlaying = true;
    }
}
