using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //setting the panels active
    //holds references of ui objects
    //unlocks levels
    //sets the stats bars values
    //displays enemies left

    [SerializeField] Image[] CollectedSamplesImage;
    [SerializeField] Image m_healthBarFiller;
    [SerializeField] Image m_fuelBarFiller;
    [SerializeField] Image m_SamplesDNABarFiller;
    [SerializeField] Image m_galaxyUnlockedImage;
    [SerializeField] TextMeshProUGUI m_galaxyUnlockedText;
    [SerializeField] TextMeshProUGUI m_enemyCountText;
    [SerializeField] TextMeshProUGUI m_coolTextInSpace;
    [SerializeField] GameObject m_coolTextPrefab;
    [SerializeField] GameObject m_onDeadPanel;
    int m_textDuration = 4;

    private void Start()
    {
        UpdateHealthUi();
        UpdateFuelUi();
        UpdateDnaSamplesBarUi();
    }

    public void UpdateHealthUi()
    {
        m_healthBarFiller.fillAmount = PlayerStats.playerHealth / PlayerStats.maxHealth;
    }

    public void UpdateFuelUi()
    {
        m_fuelBarFiller.fillAmount = PlayerStats.playerFuel / PlayerStats.maxFuel;
    }
    public void UpdateDnaSamplesBarUi()
    {
        for (int i = 0; i < PlayerStats.dnaSampleCount; i++)
        {
            CollectedSamplesImage[i].gameObject.SetActive(true);
            m_SamplesDNABarFiller.DOFillAmount(i * 0.5f, 1);
        }
    }

    public void EnableOnDeadPanel()
    {
        StartCoroutine(EnableOnDeadPanelDelayed());
    }

    IEnumerator EnableOnDeadPanelDelayed()
    {
        yield return new WaitForSeconds(3);
        m_onDeadPanel.SetActive(true);
    }

    public void SetEnemyCountText()
    {
        m_enemyCountText.text = GameManager.Instance.enemyTracker.aliveEnemyCount + "/" + GameManager.Instance.enemyTracker.maxEnemyCount;
    }

    public void ShowCoolText(string text, Vector3 pos)
    {

        GameObject coolTextObject = Instantiate(m_coolTextPrefab, pos, Quaternion.identity);
        TextMeshProUGUI coolText = coolTextObject.GetComponentInChildren<TextMeshProUGUI>();
        coolText.text = text;
        coolText.transform.DOLocalMoveY(4, m_textDuration);
        coolText.DOFade(0, m_textDuration);
        Destroy(coolTextObject, m_textDuration);
    }
    public void ShowCoolTextInSpace(string text)
    {
        m_coolTextInSpace.text = text;
        m_coolTextInSpace.DOFade(1,0);
        m_coolTextInSpace.transform.DOShakeScale(m_textDuration,0.1f, 5, 50, true).OnComplete(HideCoolTextInSpace);
    }

    void HideCoolTextInSpace()
    {
        m_coolTextInSpace.DOFade(0, m_textDuration);
    }

    public void ShowNextLevelUnlockedNotification()
    {
        m_galaxyUnlockedText.text = ("Galaxy " + PlayerStats.currentLevel.ToString() + " is unlocked!");
        m_galaxyUnlockedImage.DOFillAmount(1, 2);
        m_galaxyUnlockedText.DOFade(1, 3).OnComplete(HideNextLevelUnlockNotification); 
    }
    void HideNextLevelUnlockNotification()
    {
        m_galaxyUnlockedImage.DOFade(0,4);
        m_galaxyUnlockedText.DOFade(0, 3.5f);
    }
    }
