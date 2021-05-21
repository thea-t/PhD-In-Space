using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAndQuit : MonoBehaviour
{
    //https://docs.unity3d.com/ScriptReference/PlayerPrefs.DeleteAll.html
    //Setting player stats to the deafaut ones, because every time when i'm quitting the game they are being saved and when I want to reset the game
    //and start over, I want my stats to start as the deafout ones
    void SetText()
    {
        PlayerPrefs.SetFloat("playerHealth", 100);
        PlayerPrefs.SetFloat("playerFuel", 100);
        PlayerPrefs.SetFloat("multiplierToGatherFuel", 1);
        PlayerPrefs.SetFloat("fuelShipConsumption", 0.1f);
        PlayerPrefs.SetInt("maxHealth", 100);
        PlayerPrefs.SetInt("maxFuel", 100);
        PlayerPrefs.SetInt("damageToDeal", 5);
        PlayerPrefs.SetInt("bulletSpeed", 5);
        PlayerPrefs.SetInt("dnaSampleCount", 0);
        PlayerPrefs.SetInt("currentLevel", 0);
    }
    //Reseting player prefs by deleting all the keys and loading the Menu
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        bl_SceneLoaderUtils.GetLoader.LoadLevel("Menu");
        SetText();

    }
    //Quitting the game
    public void QuitTheGame()
    {
        Application.Quit();

        Debug.Log("app is quit");
    }

   //Pausing the game
    //https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
    public void PauseTheGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeTheGame()
    {
        Time.timeScale = 1;
    }


    //When application quits, I'm saving the player stats to certain keys in player prefs so that the players progress will be saved 
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("playerHealth", PlayerStats.playerHealth);
        PlayerPrefs.SetFloat("playerFuel", PlayerStats.playerFuel);
        PlayerPrefs.SetFloat("multiplierToGatherFuel", PlayerStats.multiplierToGatherFuel);
        PlayerPrefs.SetFloat("fuelShipConsumption", PlayerStats.fuelShipConsumption);
        PlayerPrefs.SetInt("maxHealth", PlayerStats.maxHealth);
        PlayerPrefs.SetInt("maxFuel", PlayerStats.maxFuel);
        PlayerPrefs.SetInt("damageToDeal", PlayerStats.damageToDeal);
        PlayerPrefs.SetInt("bulletSpeed", PlayerStats.bulletSpeed);
        PlayerPrefs.SetInt("dnaSampleCount", PlayerStats.dnaSampleCount);
    }
}
