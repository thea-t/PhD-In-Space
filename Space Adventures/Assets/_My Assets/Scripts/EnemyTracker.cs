using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] int m_enemyCountToDropPowerUps;
    [SerializeField] string[] m_enemyPhrace;
    Enemy[] m_enemies;

    public GameObject sampleDNAPrefab;
    public GameObject powerUpPrefab;
    public int aliveEnemyCount;
    public int maxEnemyCount;
    private void Start()
    {
        //https://answers.unity.com/questions/46283/way-to-get-all-object-with-a-certain-componentscri.html
        m_enemies = FindObjectsOfType<Enemy>();
        aliveEnemyCount = m_enemies.Length;
        maxEnemyCount = m_enemies.Length;
        GameManager.Instance.uiManager.SetEnemyCountText();
          
        if (PlayerStats.completedPlanets.Contains(SceneManager.GetActiveScene().name) == false)
        {
            //pick 1 random enemy to drop a sample
            m_enemies[Random.Range(0, m_enemies.Length)].dropsSample = true;

            //pick random enemies to drop power ups
            for (int i = 0; i < m_enemyCountToDropPowerUps; i++)
            {
                m_enemies[Random.Range(0, m_enemies.Length)].dropsPowerUp = true;
            }
        }
    }

    public string GetRandomCoolText()
    {
        if (Random.value <= 0.5f)
        {
            return m_enemyPhrace[Random.Range(0, m_enemyPhrace.Length)];
        }
        else
        {
            return null;
        }
    }

}
