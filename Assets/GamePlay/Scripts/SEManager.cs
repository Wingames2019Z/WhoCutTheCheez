using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] SE;
    [SerializeField] SettingDataModel settingDataModel;
    // Start is called before the first frame update

    private void Start()
    {
        settingDataModel = GameDataSystem.SettingDataLoad();
    }

    public void SetSettingDataModel(bool _bool)
    {
        settingDataModel.Sounds = _bool;
    }

    public void SEPlay(SEList seList)
    {

        if (!settingDataModel.Sounds)
            return;

        switch (seList)
        {
            case SEList.Pressed:
                audioSource.PlayOneShot(SE[0]);
                break;
            case SEList.GameClear:
                audioSource.PlayOneShot(SE[1]);
                break;
            case SEList.Congratulation:
                audioSource.PlayOneShot(SE[2]);
                break;
            case SEList.Applause:
                audioSource.PlayOneShot(SE[3]);
                break;
            case SEList.Oh:
                audioSource.PlayOneShot(SE[4]);
                break;
        }

    }
}

public enum SEList
{ 
    Pressed = 0,
    GameClear = 1,
    Congratulation = 2,
    Applause = 3,
    Oh = 4

}
