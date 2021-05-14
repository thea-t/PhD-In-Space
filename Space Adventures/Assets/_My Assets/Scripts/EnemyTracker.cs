using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
    Enemy[] enemies;
    public GameObject sampleDNAPrefab;
    public GameObject powerUpPrefab;
    [SerializeField] int enemyCountToDropPowerUps;
    public int aliveEnemyCount;
    public int maxEnemyCount;
    private void Start()
    {
        //https://answers.unity.com/questions/46283/way-to-get-all-object-with-a-certain-componentscri.html
        enemies = FindObjectsOfType<Enemy>();
        aliveEnemyCount = enemies.Length;
        maxEnemyCount = enemies.Length;
        GameManager.Instance.uiManager.SetEnemyCountText();

        if (PlayerStats.completedPlanets.Contains(SceneManager.GetActiveScene().name) == false)
        {
            //pick 1 random enemy to drop a sample
            enemies[Random.Range(0, enemies.Length)].dropsSample = true;

            //pick random enemies to drop power ups
            for (int i = 0; i < enemyCountToDropPowerUps; i++)
            {
                enemies[Random.Range(0, enemies.Length)].dropsPowerUp = true;
            }
        }
    }


}
