using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterController
{

    protected Animator m_animator;
    protected NavMeshAgent navMeshAgent;
    [HideInInspector] public bool dropsSample;
    [HideInInspector] public bool dropsPowerUp;
    [HideInInspector] string startChasingText;
    [HideInInspector] string onDeadText;

    #region Start,  Update and Triggers
    void Start()
    {
        m_animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //https://answers.unity.com/questions/1355590/navmeshagentisstopped-true-but-is-still-moving.html
        //https://answers.unity.com/questions/1252904/how-to-stop-navmeshagent-immediately.html
        navMeshAgent.isStopped = true;
        m_health = 100f;
    }

    protected virtual void Update()
    { // start chasing
        if (!navMeshAgent.isStopped && !isDead)
        {
            navMeshAgent.SetDestination(GameManager.Instance.playerCharacter.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
                StartChasing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            StopChasing();
        }
    }
    #endregion

    #region Functions that will be overriden by the different types of enemies
    protected virtual void StartChasing()
    {
        navMeshAgent.isStopped = false;
        GameManager.Instance.uiManager.ShowCoolText(startChasingText, transform.position);
    }

    protected virtual void StopChasing()
    {
        navMeshAgent.isStopped = true;
    }


    #endregion


    #region Functions that override CharacterController
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        m_animator.SetTrigger("Take Damage");
        //start chasing the player the moment they are hit
        StartChasing();
        Debug.Log("enemyHealth" + m_health);

    }
    protected override void Die()
    {
        base.Die();

        navMeshAgent.isStopped = true;
        m_animator.SetTrigger("Die");
        GameManager.Instance.uiManager.ShowCoolText(onDeadText, transform.position);
        GameManager.Instance.enemyTracker.aliveEnemyCount--;
        GameManager.Instance.uiManager.SetEnemyCountText();
        Destroy(gameObject, 6);

        if (dropsSample)
        {
            GameObject alienSample = Instantiate(GameManager.Instance.enemyTracker.sampleDNAPrefab, transform.position, Quaternion.identity);
        }
        if (dropsPowerUp)
        {
            GameObject randomPowerUp = Instantiate(GameManager.Instance.enemyTracker.powerUpPrefab, transform.position, Quaternion.identity);
        }
    }
    #endregion
}

