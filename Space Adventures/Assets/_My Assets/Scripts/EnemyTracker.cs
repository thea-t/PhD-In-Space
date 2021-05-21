using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
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
          
        //Checking if the list of completed planets contains the name of the scene that the player is currently on. This prevents player to 
        //keep on landing on the same planet and get the samples and unlock a level that way. He should go on every single planet if he wants to progress
        //Picking a random enemy to drop a sample

        if (PlayerStats.completedPlanets.Contains(SceneManager.GetActiveScene().name) == false)
        {
            //pick 1 random enemy to drop a sample
            m_enemies[Random.Range(0, m_enemies.Length)].dropsSample = true;

        }
    }
    //Picking a random CoolText from an array and there is 50% chance of showing that text when player enters into the enemy range
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
