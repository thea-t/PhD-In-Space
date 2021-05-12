using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    void Start()
    {
        OnGameStart();
    //   // attackAnimation = "attack";
    //}

    //protected override void StartChasing()
    //{
    //    //make the guy visable because it was invisable
    //    GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    //    m_animator.SetTrigger("spawn");
    //    StartCoroutine(StartChasingg());
    //    Debug.Log("calling");
    //}

    //IEnumerator StartChasingg()
    //{
    //    yield return new WaitForSeconds(m_animator.GetCurrentAnimatorStateInfo(0).length);
    //    m_animator.SetBool("fly", true);
    //    base.StartChasing();
    }

    protected override void StopChasing()
    {
        base.StopChasing();
        m_animator.SetBool("fly", false);
    }


    //anim event
    void OnAttack()
    {
        GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToDealDamage * PlayerStats.baseDamage * 1.5f);
    }
}
