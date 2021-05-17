using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUiAndResetAndQuit : MonoBehaviour
{
    [SerializeField] string[] m_tutorial;
    [SerializeField] GameObject m_tutorialPanel;
    [SerializeField] TextMeshProUGUI m_tutorialText;
    int m_tutorialIndex;

    private void Start()
    { //https://forum.unity.com/threads/show-instrution-image-only-once.490395/
        //https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
        if (!PlayerPrefs.HasKey("isShown"))
        {
            m_tutorialPanel.SetActive(true);
            NextTutorial();
        }
    }

    //use player prefs
    public void NextTutorial()
    {
        if (m_tutorialIndex < m_tutorial.Length)
        {
            m_tutorialText.text = m_tutorial[m_tutorialIndex];
            m_tutorialIndex++;
        }
        else
        {
            m_tutorialPanel.SetActive(false);
            PlayerPrefs.SetString("isShown", "Tutorial Ui is already shown");
        }
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("app is quit");
    }
}
