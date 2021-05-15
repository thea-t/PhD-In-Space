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

    [SerializeField] Image m_healthBarFiller;
    [SerializeField] Image m_fuelBarFiller;
    [SerializeField] Image m_SamplesDNABarFiller;
    [SerializeField] Image m_CollectedSamplesImage1;
    [SerializeField] Image m_CollectedSamplesImage2;
    [SerializeField] Image m_CollectedSamplesImage3;
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
        switch (PlayerStats.dnaSampleCount)
        {
            case 1:
                m_CollectedSamplesImage1.gameObject.SetActive(true);
                break;
            case 2:
                m_CollectedSamplesImage2.gameObject.SetActive(true);
                m_SamplesDNABarFiller.DOFillAmount(0.5f, 1);
                break;
            case 3:
                m_CollectedSamplesImage3.gameObject.SetActive(true);
                m_SamplesDNABarFiller.DOFillAmount(1, 1);
                break;
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
}
