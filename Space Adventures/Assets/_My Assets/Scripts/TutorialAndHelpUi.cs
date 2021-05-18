using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAndHelpUi : MonoBehaviour
{
    [SerializeField] string[] m_tutorial;
    [SerializeField] GameObject m_tutorialPanel;
    [SerializeField] TextMeshProUGUI m_tutorialText;
    [SerializeField] Button m_buttonStart;
    [SerializeField] Button m_buttonSettings;
    [SerializeField] Image m_uiGuideImg;
    int m_tutorialIndex;

    private void Start()
    { //https://forum.unity.com/threads/show-instrution-image-only-once.490395/
        //https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
        if (!PlayerPrefs.HasKey("isShown"))
        {
            m_buttonStart.interactable = false;
            m_buttonSettings.interactable = false;
            m_tutorialPanel.SetActive(true);
            NextTutorial();
        } 
        else if (PlayerPrefs.HasKey("isShown"))
        {
            m_buttonStart.interactable = true;
            m_buttonSettings.interactable = true;
        }
    }

    public void NextTutorial()
    {
        if (m_tutorialIndex < m_tutorial.Length)
        {
            m_tutorialText.text = m_tutorial[m_tutorialIndex];
            m_tutorialIndex++;
        }
        else
        {
            m_tutorialIndex = 0;
            m_tutorialPanel.SetActive(false);
            PlayerPrefs.SetString("isShown", "Tutorial Ui is already shown");
            m_buttonStart.interactable = true;
            m_buttonSettings.interactable = true;
        }
    }
    public void NextHelpLine()
    {
        if (m_tutorialIndex < m_tutorial.Length)
        {
            m_tutorialText.text = m_tutorial[m_tutorialIndex];
            m_tutorialIndex++;
        }
        else 
        {
            m_tutorialIndex = 0;
            m_tutorialPanel.SetActive(false);
            m_uiGuideImg.gameObject.SetActive(true);
        }
    }

}
