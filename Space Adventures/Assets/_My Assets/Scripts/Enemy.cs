using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : CharacterController
{

    protected Animator m_animator;
    protected NavMeshAgent m_navMeshAgent;
    [HideInInspector] string m_startChasingText;
    [HideInInspector] string m_onDeadText;
    [HideInInspector] public bool dropsSample;
    [HideInInspector] public bool dropsPowerUp;
    [SerializeField] int m_maxEnemyHealth;
    [SerializeField] AudioSource m_onDeadSFX;
    [SerializeField] AudioSource m_onTriggerEnterSFX;
    [SerializeField] Collider m_rangeCollider;
    #region Start,  Update and Triggers
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        //https://answers.unity.com/questions/1355590/navmeshagentisstopped-true-but-is-still-moving.html
        //https://answers.unity.com/questions/1252904/how-to-stop-navmeshagent-immediately.html
        m_navMeshAgent.isStopped = true;
        m_health = m_maxEnemyHealth;
    }

    protected virtual void Update()
    { // start chasing
        if (!m_navMeshAgent.isStopped && !isDead)
        {
            m_navMeshAgent.SetDestination(GameManager.Instance.playerCharacter.transform.position);
        }
        //else if(GameManager.Instance.playerCharacter.gameObject == null)
        //{
        //    m_navMeshAgent.isStopped = true;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            m_onTriggerEnterSFX.Play();
            GameManager.Instance.uiManager.ShowCoolText(GameManager.Instance.enemyTracker.GetRandomCoolText(), transform.position);
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
        m_navMeshAgent.isStopped = false;
        GameManager.Instance.uiManager.ShowCoolText(m_startChasingText, transform.position);
    }

    protected virtual void StopChasing()
    {
        m_navMeshAgent.isStopped = true;
    }


    #endregion


    #region Functions that override CharacterController
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //start chasing the player the moment they are hit
        StartChasing();

    }
    protected override void Die()
    {
        base.Die();
        m_navMeshAgent.isStopped = true;
        m_animator.SetTrigger("Die");
        m_onDeadSFX.Play();
        m_rangeCollider.enabled = false;
        GameManager.Instance.uiManager.ShowCoolText(m_onDeadText, transform.position);
        GameManager.Instance.enemyTracker.aliveEnemyCount--;
        GameManager.Instance.uiManager.SetEnemyCountText();
        Destroy(gameObject, 6);

        float chance = Random.value;
        Debug.Log("Chance: " + chance);
        if (dropsSample)
        {
            GameObject alienSample = Instantiate(GameManager.Instance.enemyTracker.sampleDNAPrefab, transform.position, Quaternion.identity);
        }
        if (chance <= 0.5f)
        {
            GameObject randomPowerUp = Instantiate(GameManager.Instance.enemyTracker.powerUpPrefab, transform.position, Quaternion.identity);
        }
    }
    #endregion

}

