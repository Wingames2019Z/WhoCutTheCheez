using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int Point;
    [SerializeField] GameObject InitialUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] float GasAmount;
    bool IsPlaying = false;
    bool IsReleasing = false;
    float ReleasingTime = 0f;
    float PointTime = 0f;



    //GameConfig
    //-AddGas
    private readonly float InitialGasAmount = 0f;
    private readonly float MaxGasAmount = 10f;
    private readonly float AddGasAmount = 1f;
    //-ReleaseGas
    private readonly float ReleaseGasAmount = 1f;
    //-Point
    private float AddPointTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsReleasing = false;
            ReleasingTime = 0f;
            PointTime = AddPointTime;
        }

        AddGas();
        ReleaseGas();
        PointCheck();
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
            Point += AddPointGet();
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
    public void StartButtonPressed()
    {
        InitialUI.SetActive(false);
        IsPlaying = true;
    }
    public void SetGameOver()
    {
        IsPlaying = false;
        GameOverUI.SetActive(true);
        Debug.Log("GameOver");
    }
}
