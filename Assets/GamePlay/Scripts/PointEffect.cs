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
    public void InitialSet(int point)
    {
        PointText.text = point + "pt";
        if(point >= 3 && point < 6)
        {
            PointText.fontSize = 0.4f;
            Distance = 0.08f;
        }
        else if(point >= 6)
        {
            PointText.fontSize = 0.6f;
            Distance = 0.11f;
        }
        var sequence = DOTween.Sequence();
        var targetPosition = this.transform.position.y + Distance;
        sequence.Append(this.transform.DOLocalMoveY(targetPosition, Duration))
                .Append(PointText.DOFade(0f, Duration)
                .OnComplete(() => Destroy(this.gameObject)));
    }
}
