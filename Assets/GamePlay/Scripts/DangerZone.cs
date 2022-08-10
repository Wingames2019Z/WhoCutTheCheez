using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DangerZone : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer.DOFade(0.7f, 0.5f)
            .SetLoops(6, LoopType.Yoyo)
            .OnComplete(()=>Destroy(this.gameObject));
    }
}
