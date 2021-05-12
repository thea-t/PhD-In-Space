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

    void AnimationPicker()
    {
        m_animator.SetTrigger("");
    }

    void StartAttacking()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
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
        GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToDealDamage * PlayerStats.baseDamage * 1.5f);
    }
}
