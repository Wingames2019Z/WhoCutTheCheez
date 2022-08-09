using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] Animator Animator;
    GameController GameController;
    private float DangerZone = 0.5f;

    float Speed;
    float TargetPosition;
    Tween tween;
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }
    void Update()
    {
        if (!GameController.GetIsPlaying())
            return;

        if (GameController.GetIsReleasing() && IsInDangerZone())
        {
            GameController.SetGameOver();
            GameOverAnimeSet();
        }
    }
    public void GameOverAnimeSet()
    {
        Invoke(nameof(SetAnime), 0.3f);
    }
    void SetAnime()
    {
        tween.Kill();
        Animator.SetBool("GameOver", true);
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    bool IsInDangerZone()
    {
        var inDangerZone = false;
        if(this.transform.position.x < DangerZone && this.transform.position.x > -DangerZone)
        {
            inDangerZone = true;
            AllAnimeSetGameOver();
        }
        return inDangerZone;
    }
    void AllAnimeSetGameOver()
    {
        var GameObjects = GameObject.FindGameObjectsWithTag("Pedestrian");
        foreach (var item in GameObjects)
        {
            item.GetComponent<Pedestrian>().GameOverAnimeSet();
        }
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
