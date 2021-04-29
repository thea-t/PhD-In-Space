using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    [SerializeField] ParticleSystem onShotParticle;
    [SerializeField] ParticleSystem onDeadParticle;
    [SerializeField] private Rigidbody rb;
    Animator m_animator;
    Collider m_collider;
   protected string m_animationOnAttack;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Attack()
    {
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCharacter.transform.position - transform.position, Vector3.up);
        m_animator.SetBool(m_animationOnAttack, true);
    }

    protected virtual void StopChasing()
    {
        m_animator.SetBool(m_animationOnAttack, false);
        //Do you go back to your initial point or do you stop wherever you left off
    }



    public void TakeDamage()
    {
        enemyHealth -= GameManager.Instance.playerCharacter.playerDamage;
        m_animator.SetTrigger("Take Damage");
        ParticleSystem particle = Instantiate(onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);
        Debug.Log(enemyHealth);

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        m_collider.enabled = false;
        m_animator.SetTrigger("Die");

        //particle was flying so it had to rotate in order to fix https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        ParticleSystem particle = Instantiate(onDeadParticle, new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(particle.gameObject, 8);
        Destroy(gameObject, 7);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {


            Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            StopChasing();
        }
    }


}

