using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SettingManager : MonoBehaviour
{
    [SerializeField] GameObject SettingSheet;
    [SerializeField] Image SettingCover;

    [SerializeField] TextMeshProUGUI VibrationText;
    [SerializeField] TextMeshProUGUI SEText;
    [SerializeField] float ScaleAmount = 0.2f;
    [SerializeField] SEManager sEManager;
    [SerializeField] GasSoundManager GasSoundManager;

    SettingDataModel settingDataModel;
    Vector3 ScaleButton;
    Sequence sequence;
    float AnimeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        SettingSheet.transform.localScale = Vector3.zero;
        settingDataModel = GameDataSystem.SettingDataLoad();
        TextSet();
    }

    void TextSet()
    {
        if (settingDataModel.Vibration)
        {
            VibrationText.text = "ON";
        }
        else
        {
            VibrationText.text = "OFF";
        }

        if (settingDataModel.Sounds)
        {
            SEText.text = "ON";
        }
        else
        {
            SEText.text = "OFF";
        }
    }

    public void OurGamePressed()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/developer?id=wingames2019");
#elif UNITY_IPHONE
        Application.OpenURL("https://apps.apple.com/developer/katsuya-shirai/id1453314952");
#else
    Debug.Log("Any other platform");
#endif
    }
    public void RateUsPressed()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Wingames.InstantMemory");
#elif UNITY_IPHONE
        Application.OpenURL("https://apps.apple.com/app/instantmemory/id1453314953");
#else
    Debug.Log("Any other platform");
#endif
    }
    public void VibrationPressed()
    {
        if (settingDataModel.Vibration)
        {
            settingDataModel.Vibration = false;
        }
        else
        {
            settingDataModel.Vibration = true;
            VibrationMng.ShortVibration();
        }

        GameDataSystem.SettingDataSave(settingDataModel);
        TextSet();
    }

    public void SEPressed()
    {
        if (settingDataModel.Sounds)
        {
            settingDataModel.Sounds = false;
        }
        else
        {
            settingDataModel.Sounds = true;
        }
        GameDataSystem.SettingDataSave(settingDataModel);
        sEManager.SetSettingDataModel(settingDataModel.Sounds);
        GasSoundManager.SetSettingDataModel(settingDataModel.Sounds);
        TextSet();
    }
    public bool CheckSE() => settingDataModel.Sounds;

    public void SettingOpen()
    {
        SetActiveSheet(true);
        sequence = DOTween.Sequence();
        sequence.Append(SettingSheet.transform.DOScale(Vector3.one, AnimeDuration).SetEase(Ease.OutBack))
                .Join(SettingCover.DOFade(0.4f, AnimeDuration));


    }
    public void SettingClose()
    {
        sequence = DOTween.Sequence();
        sequence.Append(SettingSheet.transform.DOScale(Vector3.zero, AnimeDuration).SetEase(Ease.InBack))
                .Join(SettingCover.DOFade(0f, AnimeDuration))
                .OnComplete(() => { SetActiveSheet(false); });
    }
    void SetActiveSheet(bool state)
    {
        SettingSheet.SetActive(state);
        SettingCover.gameObject.SetActive(state);
    }
}
