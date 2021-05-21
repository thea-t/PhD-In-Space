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
    int m_unlockedLevel;
    void Start()
    {
        //SetLevels();
    }
    //Gets the information of the current level frm playerPrefs and assigns it to m_unlockedLevel. If I had more levels it would make the buttons
    //when the player is selecting leves interactible and increase the difficulty curve of each level
    public void SetLevels()
    {
        m_unlockedLevel = PlayerPrefs.GetInt("currentLevel");
        PlayerStats.currentLevel = (Levels)m_unlockedLevel;
        IncreaseDifficultyCurve();
        for (int i = 0; i < m_unlockedLevelsButton.Length; i++)
        {
            if (i <= m_unlockedLevel)
            {
                m_unlockedLevelsButton[i].interactable = true;
                m_unlockedLevelsPanel[i].SetActive(true);
                m_lockedLevelsIcon[i].SetActive(false);

            }

        }
    }

    //Increasing the difficlty curve by increasing the multipliers to gather fuel and receive damage,depending on the currently unlocked level
    void IncreaseDifficultyCurve()
    {
        PlayerStats.multiplierToReceiveDamage = m_unlockedLevel;
        PlayerStats.multiplierToGatherFuel = m_unlockedLevel + 1;
        Debug.Log("mult. to receive damage:" + PlayerStats.multiplierToReceiveDamage);
        Debug.Log("gather fuel:" + PlayerStats.multiplierToGatherFuel);
    }
     //loads levels
    public void LoadLevel(string levelName)
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel(levelName);
    }
}
