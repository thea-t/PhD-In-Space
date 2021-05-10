using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //setting the panels active
    //holds references of ui objects
    //unlocks levels
    //sets the stats bars values
    //displays enemies left

    [SerializeField] Image healthBarFiller;
    [SerializeField] Image fuelBarFiller;

    public void UpdateHealthUi()
    {
        healthBarFiller.fillAmount = PlayerStats.playerHealth / PlayerStats.maxHealth;
    }
    public void UpdateFuelUi()
    {
        fuelBarFiller.fillAmount = PlayerStats.playerFuel / PlayerStats.maxFuel;
    }
}
