using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] Pedestrian Pedestrian;
    private float CurrentTime = 0f;
    private float StartSpawnTime = 2f;
    private float SpawnInterval = 5f;

    float SpawnPosition = 5f;

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
            CurrentTime = SpawnInterval;
            SpawnPedestrian();
        }
    }
    void SpawnPedestrian()
    {
        var positon = SpawnPositonSet();
        var target = - positon;
        var pedestrian = Instantiate(Pedestrian, new Vector3(positon, 0,0), Quaternion.identity);
        pedestrian.InitialSet(1f,target); 
    }

    float SpawnPositonSet()
    {
        var num = Random.Range(0, 2);
        var position = SpawnPosition;
        if (num == 0)
        {
            position = -SpawnPosition;
        }

        return position;
    }
}
