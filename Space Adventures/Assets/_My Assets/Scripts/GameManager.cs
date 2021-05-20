using AmazingAssets.CurvedWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerShip playerShip;
    public CurvedWorldController curvedWorldController;
    public PlayerCharacter playerCharacter;
    public LeavingAndLandingOnPlanet shipLanding;
    public UIManager uiManager;
    public EnemyTracker enemyTracker;
    public ResetAndQuit resetAndQuit;

    public static GameManager Instance { get; private set; } // static singleton

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerStats.playerHealth = PlayerPrefs.GetFloat("playerHealth");
        }
        else if (PlayerPrefs.HasKey("playerFuel"))
        {
            PlayerStats.playerFuel = PlayerPrefs.GetFloat("playerFuel");
        }
        else if (PlayerPrefs.HasKey("multiplierToGatherFuel"))
        {
            PlayerStats.multiplierToGatherFuel = PlayerPrefs.GetFloat("multiplierToGatherFuel");
        }
        else if (PlayerPrefs.HasKey("fuelShipConsumption"))
        {
            PlayerStats.fuelShipConsumption = PlayerPrefs.GetFloat("fuelShipConsumption");
        }
        else if (PlayerPrefs.HasKey("maxHealth"))
        {
            PlayerStats.maxHealth = PlayerPrefs.GetInt("maxHealth");
        }
        else if (PlayerPrefs.HasKey("maxFuel"))
        {
            PlayerStats.maxFuel = PlayerPrefs.GetInt("maxFuel");
        }
        else if (PlayerPrefs.HasKey("damageToDeal"))
        {
            PlayerStats.damageToDeal = PlayerPrefs.GetInt("damageToDeal");
        }
        else if (PlayerPrefs.HasKey("bulletSpeed"))
        {
            PlayerStats.bulletSpeed = PlayerPrefs.GetInt("bulletSpeed");
        }
        else if (PlayerPrefs.HasKey("dnaSampleCount"))
        {
            PlayerStats.dnaSampleCount = PlayerPrefs.GetInt("dnaSampleCount");
        }

        uiManager.UpdateHealthUi();
        uiManager.UpdateFuelUi();
        uiManager.UpdateDnaSamplesBarUi();
    }

}

