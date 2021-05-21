using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
   
    [SerializeField] GameObject[] m_crystals;
    int m_crystalStackHealth;

    private void Start()
    {
        //Setting the health of the crystal to its lenght because I want every single crystal to be destroyeed after it is hit once
        //Updating the fuelUi
        m_crystalStackHealth = m_crystals.Length;
        GameManager.Instance.uiManager.UpdateFuelUi();
    }

    //Removing an element of the array everytime when this function is called. I'm also destroying the crystal of the index of the health and
    //calling the Gainfuel function. If my crystal is left without health (meaning that I've called that function X times, assuming that health is X
    //all the crystals which were X at total are already destroyed) I'm destroying the parent object of the crystals as well
    public void StartGathering()
    {
       
        Debug.Log("gather fuel: " + PlayerStats.multiplierToGatherFuel);

        m_crystalStackHealth--;

            Destroy(m_crystals[m_crystalStackHealth]);
            GainFuel();

        if (m_crystalStackHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    //If my fuel is less than the max fuel minus the fuel that I'm expecting to gain, I'm gaining the expected fuel. If not, nothing happens. 
    //I'm also updating the ui panel of the fuel
    void GainFuel()
    {
        if (PlayerStats.playerFuel < PlayerStats.maxFuel-PlayerStats.multiplierToGatherFuel)
        {
            PlayerStats.playerFuel += PlayerStats.multiplierToGatherFuel;
            GameManager.Instance.uiManager.UpdateFuelUi();

            Debug.Log("fuel: " + PlayerStats.playerFuel);
        }
    }
}
