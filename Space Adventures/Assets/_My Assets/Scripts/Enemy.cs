using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Creating Enemy class that inherits from Charecter controller but is being inherited by the ranged and melee enemies because they all have something in common. 
public class Enemy : CharacterController
{

    protected Animator m_animator;
    protected NavMeshAgent m_navMeshAgent;
    [HideInInspector] public bool dropsSample;
    [HideInInspector] public bool dropsPowerUp;
    [HideInInspector] string m_onDeadText = "I will get my revenge..";
    [SerializeField] int m_maxEnemyHealth;
    [SerializeField] Collider m_rangeCollider;
    [SerializeField] AudioSource m_onDeadSFX;
    [SerializeField] AudioSource m_onTriggerEnterSFX;

    #region Start,  Update and Triggers
    //Getting components and setting stuff on start 
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        //https://answers.unity.com/questions/1355590/navmeshagentisstopped-true-but-is-still-moving.html
        //https://answers.unity.com/questions/1252904/how-to-stop-navmeshagent-immediately.html
        //Stopping the namMesh agent on start because I want enemies to chase player only when he enters their range
        m_navMeshAgent.isStopped = true;
        m_health = m_maxEnemyHealth;
    }
    
    //The Update function checks constantly if the enemy is stopped and is dead. If it is not then I'm setting the player's position as destination and the enemy starts chasing the player
    protected virtual void Update()
    { 
        if (!m_navMeshAgent.isStopped && !isDead)
        {
            m_navMeshAgent.SetDestination(GameManager.Instance.playerCharacter.transform.position);
        }
    }

    //If the player collides with the enemy's range, the enemy starts chasing the player
    //The enemy also plays some sound effects and shows a cool text in order to make the game more alive and spice it up a little
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            m_onTriggerEnterSFX.Play();
            GameManager.Instance.uiManager.ShowCoolText(GameManager.Instance.enemyTracker.GetRandomCoolText(), transform.position);
            StartChasing();
        }
    }

    //If the player leaves the range collider of the enemy, the enemy stops chasing him
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            StopChasing();
        }
    }
    #endregion

    //These functions are virtual because they will be overriden by the Melee and the Ranged enemies. The reason why am I separating these 2 classes into 
    //2 rather than keeping it just as one class called Enemy, is because they have different animators and bone structions and they just need to be 
    //separated if I want them to look cool and have custom animations
    #region Functions that will be overriden by the different types of enemies

    //When it's time for the enemy to start chasing, I'm unstopping his navMeshAgent because the Update function is checking if the NavMesh is stopped in order to set the destination
    protected virtual void StartChasing()
    {
        m_navMeshAgent.isStopped = false;
    }

    //When this function is called, I'm stopping the navMesh agent and the enemy stops moving and plays animations on his place
    protected virtual void StopChasing()
    {
        m_navMeshAgent.isStopped = true;
    }
    #endregion


    #region Functions that override CharacterController
    //I'm taking the base that is already created in the Character Controller class and just adding additionaly the Start Chasing function to be called because
    //I want the enemies to start chasing the player the moment they are damaged. If they stay on their place it is too easy to be killed and the 
    //game becomes pointless and boring. The reason why this function is not called in the CharaccterController class is because the player doesn't chase
    //but he is being chased by enemies
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //start chasing the player the moment they are hit
        StartChasing();

    }

    //After the base from the CharacterController class, I'm also stopping the navMesh agent that only enemy has. 
    //I'm also triggering the Die animation, stopping the run animation, playing cool soulnd effect that is different for each enemy,
    //disabling their range colliders and showing a cool text. This is done purely for visuals and better feeling while playing. I don't want the game to be too flat.
    //Something important that should not be forgotten is to update the Ui showing the alicve enemy count by calling the SetEnemyCountText function and decreasing the alive enemy count by 1
    //6 seconds after enemy is dead I'm destroying it because there is no point of staying in the scene anymore
    protected override void Die()
    {
        base.Die();
        m_navMeshAgent.isStopped = true;
        m_animator.SetBool("Run", false);
        m_animator.SetTrigger("Die");
        m_onDeadSFX.Play();
        m_rangeCollider.enabled = false;
        GameManager.Instance.uiManager.ShowCoolText(m_onDeadText, transform.position);
        GameManager.Instance.enemyTracker.aliveEnemyCount--;
        GameManager.Instance.uiManager.SetEnemyCountText();
        Destroy(gameObject, 6);

        //Because I noticed that sometimes enemies might damage too much the player and there are not that many ways to restore health, I decided to
        //let player gain some health as a reward after he kills an enemy. This will also encourage the player to go fight more because it's worth
        //fighting when there is a reward.
        if (PlayerStats.playerHealth<=PlayerStats.maxHealth-PlayerStats.gainHealthOnEnemyDeath)
        {
            PlayerStats.playerHealth += PlayerStats.gainHealthOnEnemyDeath;
            GameManager.Instance.uiManager.ShowNotificationText("Health gained!");
        }
        
        //There is 50% chance of dropping a powerUp which will increase some of Player's stats
        float chance = Random.value;
        Debug.Log("Chance: " + chance);
        if (chance <= 0.5f)
        {
            GameObject randomPowerUp = Instantiate(GameManager.Instance.enemyTracker.powerUpPrefab, transform.position, Quaternion.identity);
        }

        //The enemyTracker class is picking a random enemy out of all to drop a sample. If this is the enemy that is picked to drop a sample, I'm 
        //instantiating a sample after the enemy dies. This is done in order to prevent multiple enemies of droppig samples because samples are very valuable
        //There are 3 planets in the galaxy and only 3 samples should be dropped and picked. Otherwise it will be too easy to unlock a level.

        if (dropsSample)
        {
            GameObject alienSample = Instantiate(GameManager.Instance.enemyTracker.sampleDNAPrefab, transform.position, Quaternion.identity);
        }
    }
    #endregion

}

