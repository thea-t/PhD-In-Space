using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void OnChaseBegin()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        m_animator.SetTrigger("spawn");
        StartCoroutine(StartChasing());
        StartAttacking();
    }

    IEnumerator StartChasing()
    {
        yield return new WaitForSeconds(m_animator.GetCurrentAnimatorStateInfo(0).length);
        m_animator.SetBool("fly", true);
        base.OnChaseBegin();
    }

    protected override void StopChasing()
    {
        base.StopChasing();
        m_animator.SetBool("fly", false);
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
        GameManager.Instance.playerCharacter.TakeDamage();
    }
}
