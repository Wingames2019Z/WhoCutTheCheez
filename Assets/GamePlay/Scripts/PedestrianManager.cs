using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] Pedestrian Pedestrian;

    [SerializeField] private float []SpawnInterval;
    [SerializeField] private float []SpawnIntervalRandom;
    [SerializeField] private float []PedestrianSpeed;
    [SerializeField] private float []PedestrianSpeedRandom;

    private float CurrentTime = 0f;
    private float StartSpawnTime = 2f;

    private int Level = 0;



    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.GetIsPlaying())
            return;

        CurrentTime -= Time.deltaTime;
        if(CurrentTime < 0)
        {
            CurrentTime = SpawnIntervalSet();
            SpawnPedestrian();
        }

        float SpawnIntervalSet()
        {
            var spawnTime = Random.Range(SpawnInterval[Level], SpawnInterval[Level] + SpawnIntervalRandom[Level]);
            return spawnTime;
        }
    }
    public int GetLevel() => Level;
    void SpawnPedestrian()
    {
        var positon = SpawnPositonSet();
        var target = - positon.x;
        var rotation = 90;

        if(target < 0)
        {
            rotation = rotation * -1;
        }

        var pedestrian = Instantiate(Pedestrian, positon, Quaternion.Euler(0f, rotation, 0f));
        var speed = Random.Range(PedestrianSpeed[Level], PedestrianSpeed[Level] + PedestrianSpeedRandom[Level]);
        pedestrian.InitialSet(speed, target); 
    }

    Vector3 SpawnPositonSet()
    {
        var positionX = 2f;
        var positionZ = 2f;
        var num = Random.Range(0, 2);
        if (num == 0)
        {
            positionX = -positionX;
            positionZ = 1.5f;
        }
        return new Vector3(positionX, 0 ,positionZ);
    }

    public void SpawnSetting(int level)
    {
        Level = level;
    }
}
