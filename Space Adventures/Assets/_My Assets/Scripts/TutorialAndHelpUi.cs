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
        //If the tutorial hasn't been shown already, I'm not allowing to the player to click any buttons(by making them uninteractable) untill the player
        //finishes the tutorial. I'm saving this information of isShown in the player prefs so that the tutorial won't play every single time whe the game 
        //is loaded
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
    //Everytime when this function is called when a button is pressed. (I've done this from the inspector)
    //This function moves to the next tutorial by increasing the tutorial index by 1 everytime the button is pressed, until the end of the array is reached
    //When the end of the array is reached, the panel is set inactive and the buttons become interactable
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
    //It does the same thing as the Next tutorial function, but it doesnt save the information in player prefs, because I want the helplines to be 
    //shown every single time when the help button is being pressed. In the NextTutorial function my idea was to show the tutoria only once in the beginning
    //of the game. This is why I used playerPrefs there and not here
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
