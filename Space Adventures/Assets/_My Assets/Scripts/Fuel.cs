using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
   
    [SerializeField] GameObject[] m_crystals;
    int m_crystalStackHealth;

    private void Start()
    {
        m_crystalStackHealth = m_crystals.Length;
        GameManager.Instance.uiManager.UpdateFuelUi();
    }

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

    void GainFuel()
    {
        if (PlayerStats.playerFuel < PlayerStats.maxFuel)
        {
            PlayerStats.playerFuel += PlayerStats.multiplierToGatherFuel;
            GameManager.Instance.uiManager.UpdateFuelUi();

            Debug.Log("fuel: " + PlayerStats.playerFuel);
        }
    }
}
