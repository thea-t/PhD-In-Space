using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] AudioSource m_onHitSFX;

    //Since the enemy is constantly moving, I should check all the time if it time for attacking. The enemy is a melee so the goal is to attack
    //when it gets close to the player. This is why I'm checking if the remaining distance is smaller than the stopping distance that is set in the 
    //navMeshAgent in the inspector which means that enemy is close to the player. Then if the player is alive im calling the attack animation which has
    //an animation event and calls the OnAttack function. 
    //If I don't check if the player is alive, the enemy will continue attacking even if the player is dead, which is fine but I'm not aiming for this effect
    protected override void Update()
    {
        base.Update();
        StartAttacking();
    }
    void StartAttacking()
    {
        if (!GameManager.Instance.playerCharacter.isDead && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            m_animator.SetBool("attack", true);
        }
        else
        {
            m_animator.SetBool("attack", false);
        }
    }
    //In addition to the base of these functions that I've created in the Enemy class, I'm setting the conditions of their animations to true/false 
    protected override void StartChasing()
    {
        base.StartChasing();
        m_animator.SetBool("fly", true);
    }

    protected override void StopChasing()
    {
        base.StopChasing();
        m_animator.SetBool("fly", false);
    }

    
    //When the enemy's animation reaches to a certain second, I'm dealing damage to the player with the value of the base damage to deal + the multiplier to 
    //receive damage, which increases over the levels

    //anim event
    void OnAttack()
    {
        m_onHitSFX.Play();
        GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToReceiveDamage + PlayerStats.damageToDeal);
    }
}
