using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Update()
    {
        base.Update();
        StartAttacking();
    }

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

    void StartAttacking()
    {
        if (!GameManager.Instance.playerCharacter.isDead && m_navMeshAgent.remainingDistance < m_navMeshAgent.stoppingDistance)
        {
            m_animator.SetBool("attack", true);
        }
        else
        {
            m_animator.SetBool("attack", false);
        }
    }

    //anim event
    void OnAttack()
    {
            GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToReceiveDamage + PlayerStats.damageToDeal);
    }
}
