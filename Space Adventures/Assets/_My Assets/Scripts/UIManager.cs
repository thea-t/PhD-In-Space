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

    [SerializeField] Image healthBarFiller;
    [SerializeField] Image fuelBarFiller;
    [SerializeField] Image SamplesDNABarFiller;
    [SerializeField] GameObject onDeadPanel;
    [SerializeField] Image m_CollectedSamplesImage1;
    [SerializeField] Image m_CollectedSamplesImage2;
    [SerializeField] Image m_CollectedSamplesImage3;
    [SerializeField] TextMeshProUGUI m_enemyCountText;
    [SerializeField] GameObject coolTextPrefab;

    private void Start()
    {
        UpdateHealthUi();
        UpdateFuelUi();
        UpdateDnaSamplesBarUi();
    }

    public void UpdateHealthUi()
    {
        healthBarFiller.fillAmount = PlayerStats.playerHealth / PlayerStats.maxHealth;
    }

    public void UpdateFuelUi()
    {
        fuelBarFiller.fillAmount = PlayerStats.playerFuel / PlayerStats.maxFuel;
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
                SamplesDNABarFiller.DOFillAmount(0.5f, 1);
                break;
            case 3:
                m_CollectedSamplesImage3.gameObject.SetActive(true);
                SamplesDNABarFiller.DOFillAmount(1, 1);
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
        onDeadPanel.SetActive(true);
    }

    public void SetEnemyCountText()
    {
        m_enemyCountText.text = GameManager.Instance.enemyTracker.aliveEnemyCount + "/" + GameManager.Instance.enemyTracker.maxEnemyCount;
    }

    public void ShowCoolText(string text, Vector3 pos)
    {
        int textDuration = 3;

        GameObject coolTextObject = Instantiate(coolTextPrefab, pos, Quaternion.identity);
        TextMeshProUGUI coolText = coolTextObject.GetComponentInChildren<TextMeshProUGUI>();
        coolText.text = text;
        coolText.transform.DOLocalMoveY(4, textDuration);
        coolText.DOFade(0, textDuration);
        Destroy(coolTextObject, textDuration);
    }

}
