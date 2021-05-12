using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] GameObject[] crystals;
    int crystalStackHealth;

    private void Start()
    {
        crystalStackHealth = crystals.Length;
        GameManager.Instance.uiManager.UpdateFuelUi();
    }

    public void StartGathering()
    {
        Debug.Log("gathering");

        crystalStackHealth--;

            Destroy(crystals[crystalStackHealth]);
            GainFuel();

        if (crystalStackHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void GainFuel()
    {
        if (PlayerStats.playerFuel < PlayerStats.maxFuel)
        {
            PlayerStats.playerFuel += PlayerStats.multiplierFuel;
            Debug.Log("fuel: " + PlayerStats.playerFuel);
            GameManager.Instance.uiManager.UpdateFuelUi();
        }
    }
}
