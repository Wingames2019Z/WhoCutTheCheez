using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSoundManager : MonoBehaviour
{
    [SerializeField] GameController GameController;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip []GasSounds;

    // Update is called once per frame
    private void Update()
    {
        if (!GameController.GetIsPlaying())
            return;

        if (GameController.GetIsReleasing())
        {
            AudioSource.Play();
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(GetGasSound());
            }
        }
        else
        {
            AudioSource.Stop();
        }
    }
    public void GameOverSound()
    {
        AudioSource.Play();
        AudioSource.PlayOneShot(GetGasSound());
    }
    AudioClip GetGasSound()
    {
        var num = Random.Range(0, GasSounds.Length);
        return GasSounds[num];
    }

}
