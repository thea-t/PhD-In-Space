using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    [SerializeField] ParticleSystem onShotParticle;
    [SerializeField] ParticleSystem onDeadParticle;
    [SerializeField] Collider m_bodyCollider;
    Collider m_rangeCollider;
    protected Animator m_animator;
    public GameObject m_DNAsample;
    protected NavMeshAgent navMeshAgent;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_rangeCollider = GetComponent<Collider>();
        //https://answers.unity.com/questions/1355590/navmeshagentisstopped-true-but-is-still-moving.html
        //https://answers.unity.com/questions/1252904/how-to-stop-navmeshagent-immediately.html
        navMeshAgent.isStopped = true;
    }

    private void FixedUpdate()
    { // start chasing
        if (navMeshAgent.isStopped == false)
        {
            navMeshAgent.SetDestination(GameManager.Instance.playerCharacter.transform.position);
        }
       
       
    }

    protected virtual void OnChaseBegin()
    {
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCharacter.transform.position - transform.position, Vector3.up);
        navMeshAgent.isStopped = false;
    }

    protected virtual void StopChasing()
    {
        navMeshAgent.isStopped = true;
    }

    public void TakeDamage()
    {
        enemyHealth -= PlayerStats.playerDamage;
        m_animator.SetTrigger("Take Damage");
        ParticleSystem particle = Instantiate(onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);
        Debug.Log(enemyHealth);

        if (enemyHealth <= 0)
        {
            Die();
            GameObject alienSample = Instantiate(m_DNAsample, transform.position, Quaternion.identity); ;
        }
    }

    void Die()
    {
        navMeshAgent.isStopped = true;
        m_bodyCollider.enabled = false;
        m_rangeCollider.enabled = false; 
        m_animator.SetTrigger("Die");

        //particle was flying so it had to rotate in order to fix https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        ParticleSystem particle = Instantiate(onDeadParticle, new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(particle.gameObject, 5);
        Destroy(gameObject, 6);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            OnChaseBegin();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("playerBody"))
        {
            StopChasing();
        }
    }


}

