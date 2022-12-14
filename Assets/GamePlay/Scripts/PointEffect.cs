using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PointEffect : MonoBehaviour
{
    [SerializeField] TextMeshPro PointText;
    float Duration = 1f;
    float Distance = 0.05f;
    float DistanceRange = 0.02f;
    public void InitialSet(int point)
    {
        PointText.fontSize = 0.4f;       
        Distance = Random.Range(Distance - DistanceRange, Distance + DistanceRange);
        PointText.text ="+"+ point + "pt";
        //this.transform.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        var targetPosition = this.transform.position.y + Distance;
        sequence.Append(this.transform.DOLocalMoveY(targetPosition, Duration))
                .Join(this.transform.DOScale(Vector3.one,Duration/2).SetEase(Ease.OutBack))
                .Append(PointText.DOFade(0f, Duration)
                .OnComplete(() => Destroy(this.gameObject)));
    }
}
