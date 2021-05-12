using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterController
{
    [SerializeField] Collider m_bodyCollider;
    protected Animator m_animator;
    public GameObject m_DNAsample;
    protected NavMeshAgent navMeshAgent;
    protected string attackAnimation;


    #region Triggers and Updates
    private void FixedUpdate()
    { // start chasing
        if (!navMeshAgent.isStopped)
        {
            navMeshAgent.SetDestination(GameManager.Instance.playerCharacter.transform.position);
        }
        StartAttacking();
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
    protected void OnGameStart()
    {
        m_animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //https://answers.unity.com/questions/1355590/navmeshagentisstopped-true-but-is-still-moving.html
        //https://answers.unity.com/questions/1252904/how-to-stop-navmeshagent-immediately.html
        navMeshAgent.isStopped = true;
        m_health = 100f;
    }

    protected virtual void StartChasing()
    {
        //transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCharacter.transform.position - transform.position, Vector3.up);
        navMeshAgent.isStopped = false;
    }

    protected virtual void StopChasing()
    {
        navMeshAgent.isStopped = true;
    }
    void StartAttacking()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_animator.SetBool(attackAnimation, true);
        }
        else
        {
            m_animator.SetBool(attackAnimation, false);
        }
    }

    #endregion


    #region Functions that override CharacterController
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        m_animator.SetTrigger("Take Damage");

        Debug.Log("enemyHealth" + m_health);

    }
    protected override void Die()
    {
        base.Die();
        navMeshAgent.isStopped = true;
        m_bodyCollider.enabled = false;
        m_animator.SetTrigger("Die");
        GameObject alienSample = Instantiate(m_DNAsample, transform.position, Quaternion.identity);
    }
    #endregion
}

