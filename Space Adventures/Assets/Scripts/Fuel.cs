using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] List<GameObject> crystals;
    int playerGatheringPower = 5;
    int fuelHealth;

    private void Start()
    {
        fuelHealth = crystals.Count * 10;

        //Debug.Log("gathering power: " + PlayerStats.playerGatheringPower);
    }

    public void StartGathering()
    {
        Debug.Log("gathering");
        fuelHealth -= playerGatheringPower;
        //Debug.Log("fuel health: " + fuelHealth);
        UpdateCrystalLook();
    }

    //
    void UpdateCrystalLook()
    {
        for (int i = 0; i < crystals.Count; i++)
        {
            if (fuelHealth % crystals.Count == 0)
            {
                //Debug.Log("destroyed crystal: " + crystals[i]);
                Destroy(crystals[i]);
                crystals.Remove(crystals[i].gameObject);

                //Debug.Log("list count: " + crystals.Count);

                GainFuel();
                OnCrystalDestroyed();
            }
        }
    }
   

    void GainFuel()
    {
        if (PlayerStats.playerFuel < PlayerStats.maxFuel)
        {
            PlayerStats.playerFuel += PlayerStats.fuelMultiplier;
            Debug.Log("fuel: " + PlayerStats.playerFuel);
        }
    }

    void OnCrystalDestroyed()
    {
        if (crystals.Count <=0)
        {
            Destroy(this.gameObject, 3);
        }
    }
}
