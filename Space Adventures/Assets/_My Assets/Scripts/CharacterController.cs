using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The CharacterController class will be responsible of the common functionalities that each character have. This is done in order to prevent duplicate code
public class CharacterController : MonoBehaviour
{
    [SerializeField] ParticleSystem m_onShotParticle;
    [SerializeField] ParticleSystem m_onDeadParticle;
    [SerializeField] Collider m_bodyCollider;
    public bool isDead;
    protected float m_damage;
    protected float m_health;

    //Every character, no matter if it is enemy or player take damage. This function will be called every single time when someone in the game takes damage
    //It expects to receive damage as a parameter. It als instantiates a particle and destroys it in 2 seconds
    //This function also checks if the health of the player is < 0 in order to call the Die function. This could have been done also in Update, but there is 
    //no point of checking the health every single second. It is better this check to be done only when health is reduced 
    public virtual void TakeDamage(float damage)
    {
        m_health -= damage;

        ParticleSystem particle = Instantiate(m_onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);

        if (m_health <= 0)
        {
            Die();
        }
    }

    //Called when someone dies. Every character has a particle when he dies, so this function instantiates it and destroys it in 5 seconds.
    //It also sets a bool as true, which is used to stop the enemy from chasing the player when one of them is dead.
    //The body collider of the dead character is also being disabled because if it stays enabled, there are bugs for example the player can keep shooting at the enemy before it is being destroyed
  
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
