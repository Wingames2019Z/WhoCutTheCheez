using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSoundManager : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip []GasSounds;
    SettingDataModel settingDataModel;
    void Start()
    {
        settingDataModel = GameDataSystem.SettingDataLoad();
    }

    private void Update()
    {
        if (!settingDataModel.Sounds)
            return;

        if (!GameController.GetIsPlaying())
            return;

        if (GameController.GetIsReleasing())
        {
            AudioSource.Play();
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(GetGasSound());
                if (!settingDataModel.Vibration)
                    return;
                VibrationMng.ShortVibration();
            }
        }
        else
        {
            AudioSource.Stop();
        }
    }
    public void SetSettingDataModel(SettingDataModel _settingDataModel)
    {
        settingDataModel = _settingDataModel;
    }
    public void GameOverSound()
    {
        if (!settingDataModel.Sounds)
            return;
        AudioSource.Play();
        AudioSource.PlayOneShot(GetGasSound());
    }
    AudioClip GetGasSound()
    {
        var num = Random.Range(0, GasSounds.Length);
        return GasSounds[num];
    }
}
