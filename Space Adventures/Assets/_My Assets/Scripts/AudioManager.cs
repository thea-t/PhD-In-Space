using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //https://answers.unity.com/questions/1369959/how-can-i-use-a-sliders-unityevent-on-value-change.html
    //https://answers.unity.com/questions/16603/is-there-a-global-volume-setting.html


    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
