using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class VibrationMng
{
    // êUìÆ
    public static void ShortVibration()
    {
        if (SystemInfo.supportsVibration)
        {
            PlaySystemSound(1519);
            Vibrate(3);
        }
    }

    // iOSê›íË
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern void _playSystemSound(int n);
#endif

    private static void PlaySystemSound(int n) //à¯êîÇ…IDÇìnÇ∑
    {
#if UNITY_IOS && !UNITY_EDITOR
            _playSystemSound(n);
#endif
    }

    // Androidê›íË
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif

    private static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("vibrate", milliseconds);
#endif
        if (milliseconds >= 1000)
        {
            Handheld.Vibrate();
        }
    }
}