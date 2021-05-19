using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] ParticleSystem m_onShotParticle;
    [SerializeField] ParticleSystem m_onDeadParticle;
    [SerializeField] Collider m_bodyCollider;
     public bool isDead;
    protected float m_damage;
    protected float m_health;


    public virtual void TakeDamage(float damage)
    {
        m_health -= damage;
        Debug.Log("receiveDamage:" + damage);
        Debug.Log("players health:" + PlayerStats.playerHealth);

        ParticleSystem particle = Instantiate(m_onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);

        if (m_health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

        isDead = true;
        GameManager.Instance.playerCharacter.canMove = false;
        m_bodyCollider.enabled = false;
        //particle was flying so it had to rotate in order to fix https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        ParticleSystem particle = Instantiate(m_onDeadParticle, new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(particle.gameObject, 5);
    }

}
