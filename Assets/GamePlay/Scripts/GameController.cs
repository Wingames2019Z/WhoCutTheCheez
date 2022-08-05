using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject InitialUI;
    [SerializeField] float GasAmount;
    bool IsPlaying = false;

    float span = 1f;
    float currentTime = 0f;

    //GameConfig
    private readonly float InitialGasAmount = 50f;
    private readonly float MaxGasAmount = 100f;
    private readonly float AddGasAmount = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GasAmount = InitialGasAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlaying)
            return;
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            AddGas();
            currentTime = 0f;
        }
    }
    void AddGas()
    {
        GasAmount += AddGasAmount;
        if (GasAmount > MaxGasAmount)
        {
            Debug.Log("Game Over");
        }
    }
    public void StartButtonPressed()
    {
        InitialUI.SetActive(false);
        IsPlaying = true;
    }
}
