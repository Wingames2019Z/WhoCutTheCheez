using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] Animator Animator;
    [SerializeField] SEManager SEManager;
    GameController GameController;
    private float DangerZone = 0.5f;
    private float NearMissZone = 0.2f;
    float Speed;
    float TargetPosition;
    Tween tween;

    bool[] NearMiss = new bool []{ false, false };
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SEManager = GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>();
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

    public void NearMissCheck()
    {

        if(this.gameObject.transform.position.x > DangerZone && this.gameObject.transform.position.x <= DangerZone + NearMissZone)
        {
            if (NearMiss[0])
                return;
            NearMiss[0] = true;
            GameController.NearMiss(this.gameObject.transform.position.x);
        }

        if (this.gameObject.transform.position.x < -DangerZone && this.gameObject.transform.position.x >= -DangerZone - NearMissZone)
        {
            if (NearMiss[1])
                return;
            NearMiss[1] = true;
            GameController.NearMiss(this.gameObject.transform.position.x);
        }
    }

    public void GameOverAnimeSet()
    {
        Invoke(nameof(SetAnime), 0.3f);
    }
    void SetAnime()
    {
        tween.Kill();
        SEManager.SEPlay(SEList.Oh);
        Animator.SetBool("GameOver", true);
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
        ReturnPosition();
    }

    void ReturnPosition()
    {
        if (this.transform.position.x > DangerZone)
        {
            this.transform.DOMove(new Vector3(DangerZone, 0, this.transform.position.z), 0.1f);
        }
        else if (this.transform.position.x < -DangerZone)
        {
            this.transform.DOMove(new Vector3(-DangerZone, 0, this.transform.position.z), 0.1f);
        }
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
            .SetEase(Ease.Linear)
            .OnComplete(()=> { Destroy(this.gameObject); });
    }
    public void InitialSet(float speed, float targetPosition)
    {
        Speed = speed;
        TargetPosition = targetPosition;
        Move();
    }
}
