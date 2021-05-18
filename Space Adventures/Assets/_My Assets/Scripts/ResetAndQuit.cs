using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAndQuit : MonoBehaviour
{
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        bl_SceneLoaderUtils.GetLoader.LoadLevel("Menu");
    }

    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("app is quit");
    }
}
