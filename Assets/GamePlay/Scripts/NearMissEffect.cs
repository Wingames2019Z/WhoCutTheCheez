using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class NearMissEffect : MonoBehaviour
{
    [SerializeField] TextMeshPro PointText;
    float Duration = 1f;
    float Distance = 0.05f;
    float DistanceRange = 0.02f;
    public void Start()
    {
        PointText.fontSize = 0.4f;
        Distance = Random.Range(Distance - DistanceRange, Distance + DistanceRange);

        var sequence = DOTween.Sequence();
        var targetPosition = this.transform.position.y + Distance;
        sequence.Append(this.transform.DOLocalMoveY(targetPosition, Duration))
                .Join(this.transform.DOScale(Vector3.one, Duration / 2).SetEase(Ease.OutElastic))
                .Append(PointText.DOFade(0f, Duration)
                .OnComplete(() => Destroy(this.gameObject)));
    }
}
