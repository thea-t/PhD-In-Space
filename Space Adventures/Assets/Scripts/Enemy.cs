using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    [SerializeField] ParticleSystem onShotParticle;
    [SerializeField] ParticleSystem onDeadParticle;
    Animator anim;
    Collider col;


    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    protected virtual void Attack()
    {

    }

    public void TakeDamage()
    {
        enemyHealth -= GameManager.Instance.playerCharacter.playerDamage;
        anim.SetTrigger("Take Damage");
        ParticleSystem particle = Instantiate(onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);
        Debug.Log(enemyHealth);

        if (enemyHealth <= 0)
        {
            Die();
        }


    }

    void ChasePlayer()
    {

    }

    void Die()
    {
        col.enabled = false;
        anim.SetTrigger("Die");

        //particle was flying so it had to rotate in order to fix https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        ParticleSystem particle = Instantiate(onDeadParticle, new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(particle.gameObject, 8);
        Destroy(gameObject, 7);
    }
}

