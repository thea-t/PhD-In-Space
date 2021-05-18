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

    public static GameManager Instance { get; private set; } // static singleton

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("health"))
        {
            PlayerStats.playerHealth = PlayerPrefs.GetFloat("health");
            GameManager.Instance.uiManager.UpdateHealthUi();
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("health", PlayerStats.playerHealth);
    }

}

