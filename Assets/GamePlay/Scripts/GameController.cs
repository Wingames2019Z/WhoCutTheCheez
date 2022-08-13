using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GasSoundManager GasSoundManager;
    [SerializeField] PedestrianManager PedestrianManager;
    [SerializeField] SEManager SEManager;
    [SerializeField] EffectManager EffectManager;
    [SerializeField] UIManager UIManager;
    [SerializeField] Animator MainCharaAnimetor;
    [SerializeField] int Point;
    [SerializeField] GameObject InitialUI;
    [SerializeField] GameObject PlayingUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject DanzerZone;
    [SerializeField] Result Result;
    [SerializeField] float GasAmount;
    [SerializeField] int[] DifficultyLevel;
    [SerializeField] int Level;
    UserDataModel UserDataModel;
    bool IsPlaying = false;
    bool IsReleasing = false;
    bool IsOverFlow = false;
    [SerializeField] float ReleasingTime = 0f;
    [SerializeField] float PointTime = 0f;

    //GameConfig
    //-AddGas
    private readonly float InitialGasAmount = 0f;
    private readonly float MaxGasAmount = 5f;
    private readonly float AddGasAmount = 1f;
    //-ReleaseGas
    private readonly float ReleaseGasAmount = 1f;
    //-Point
    private float AddPointTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        UserDataModel = GameDataSystem.UserDataLoad();
        PlayingUI.SetActive(false);
        PointTime = AddPointTime;
        GasAmount = InitialGasAmount;
        PedestrianManager.SpawnSetting(Level);
    }
    public int GetLevel() => Level;
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
            if (IsOverFlow)
                return;
            NearMissCheck();
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
            OverFlow();
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
                if (IsOverFlow)
                {
                    IsOverFlow = false;
                }
            }
            ReleasingTime += Time.deltaTime;
        }
    }
    void PointCheck()
    {
        if(ReleasingTime >= PointTime)
        {
            //var addPoint = AddPointGet();
            var addPoint = 1;
            Point += addPoint;
            LevelSetting(Point);
            EffectManager.InstantiatePointEffect(addPoint);
            EffectManager.InstantiateGas();
            UIManager.ScoreSet(Point);
            PedestrianManager.SpawnSetting(Level);
            if (UserDataModel.BestScore < Point)
            {
                UserDataModel.BestScore = Point;
                UIManager.BestScoreSet(UserDataModel.BestScore);
            }
            PointTime += AddPointTime;
        }
    }
    void NearMissCheck()
    {
        var obj = GameObject.FindGameObjectsWithTag("Pedestrian");
        foreach (var item in obj)
        {
            item.GetComponent<Pedestrian>().NearMissCheck();
        }
    }

    public void NearMiss(float xPosition)
    {
        var nearMissPoint = 5;
        EffectManager.InstantiateNearMissEffect(xPosition);
        Point += nearMissPoint;
        LevelSetting(Point);
        UIManager.ScoreSet(Point);
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
        DanzerZone.SetActive(true);
    }
    public void OverFlow()
    {
        MainCharaAnimetor.SetBool("OverFlow", true);
        IsReleasing = true;
        IsOverFlow = true;
    }

    public void SetGameOver()
    {
        IsPlaying = false;
        IsReleasing = false;
        GameOverUI.SetActive(true);
        ReleasingTime = 0f;
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
        MainCharaAnimetor.SetBool("OverFlow", false);
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

    public void LevelSetting(int score)
    {
        if (score > DifficultyLevel[0] && score <= DifficultyLevel[1])
        {
            Level = 0;
        }
        else if (score > DifficultyLevel[1] && score <= DifficultyLevel[2])
        {
            Level = 1;
        }
        else if (score > DifficultyLevel[2] && score <= DifficultyLevel[3])
        {
            Level = 2;
        }
        else if (score > DifficultyLevel[3] && score <= DifficultyLevel[4])
        {
            Level = 3;
        }
        else if (score > DifficultyLevel[4] && score <= DifficultyLevel[5])
        {
            Level = 4;
        }
        else if (score > DifficultyLevel[5])
        {
            Level = 5;
        }

    }
}
