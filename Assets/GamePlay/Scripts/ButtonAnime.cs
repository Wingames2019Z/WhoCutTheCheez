using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ButtonAnime : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] RectTransform ButtonRect;
    [SerializeField] float ScaleAmount = 0.2f;
    [SerializeField] UnityEvent EndEvent;
    SEManager SEManager;
    Vector3 ScaleButton;
    bool IsPlaying = false;
    // Start is called before the first frame update
    private void Start()
    {
        SEManager = GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>();
        ScaleButton = new Vector3(ScaleAmount, ScaleAmount, ScaleAmount);
        Button.onClick.AddListener(()=> { ButtonPressed();});
    }
    public void ButtonPressed()
    {
        if (IsPlaying)
            return;
        SEManager.SEPlay(SEList.Pressed);
        IsPlaying = true;
        ButtonRect.DOPunchScale(ScaleButton, 0.2f).SetEase(Ease.OutElastic)
            .OnComplete(() => EndAnime());
    }
    void EndAnime()
    {
        IsPlaying = false;
        EndEvent.Invoke();
    }
    private void Reset()
    {
        Button = this.GetComponent<Button>();
        ButtonRect = this.GetComponent<RectTransform>();
    }
}
