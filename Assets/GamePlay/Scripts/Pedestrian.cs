using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pedestrian : MonoBehaviour
{
    GameController GameController;
    private float DangerZone = 2f;

    float Speed;
    float TargetPosition;
    Tween tween;
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }
    void Update()
    {
        if (GameController.GetIsReleasing() && IsInDangerZone() && GameController.GetIsPlaying())
        {
            GameController.SetGameOver();
        }
    }

    bool IsInDangerZone()
    {
        var inDangerZone = false;
        if(this.transform.position.x < DangerZone && this.transform.position.x > -DangerZone)
        {
            inDangerZone = true;
            tween.Kill();
        }
        return inDangerZone;
    }
    void Move()
    {
        tween = this.transform.DOMoveX(TargetPosition, Speed)
            .SetSpeedBased()
            .OnComplete(()=> { Destroy(this.gameObject); });
    }
    public void InitialSet(float speed, float targetPosition)
    {
        Speed = speed;
        TargetPosition = targetPosition;
        Move();
    }
}
