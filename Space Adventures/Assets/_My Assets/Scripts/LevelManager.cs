using Lovatto.SceneLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Levels
{
    MilkyWay,
    Tadpole,
    Andromeda,
    Pinwheel,
    Triangulum
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] Button[] m_unlockedLevelsButton;
    [SerializeField] GameObject[] m_unlockedLevelsPanel;
    [SerializeField] GameObject[] m_lockedLevelsIcon;

    void Start()
    {
        SetLevels();
    }

    public void SetLevels()
    {
        int unlockedLevel = PlayerPrefs.GetInt("currentLevel");
        PlayerStats.currentLevel = (Levels)unlockedLevel;

        for (int i = 0; i < m_unlockedLevelsButton.Length; i++)
        {
            if (i <= unlockedLevel)
            {
                m_unlockedLevelsButton[i].interactable = true;
                m_unlockedLevelsPanel[i].SetActive(true);
                m_lockedLevelsIcon[i].SetActive(false);

                //remplace i with unlockedLevel and move it out of the for loop
                PlayerStats.multiplierToReceiveDamage = i;
                PlayerStats.multiplierToGatherFuel = i + 1;

            }

        }
    }

    public void LoadLevel(string levelName)
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel(levelName);
    }
}
