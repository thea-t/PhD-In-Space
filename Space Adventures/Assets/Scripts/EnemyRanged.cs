using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] GameObject bulletShootPoint;



    protected override void StartChasing()
    {
        base.StartChasing();
        m_animator.SetBool("Run", true);
        m_animator.SetBool("Projectile Right Attack 01", true);
    }

    protected override void StopChasing()
    {
        base.StopChasing();
        m_animator.SetBool("Run", false);
        m_animator.SetBool("Projectile Right Attack 01", false);
    }

    //animator event
    void OnShoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletShootPoint.transform.position, transform.rotation);
        Vector3 shootPointPos = new Vector3(bulletShootPoint.transform.position.x, 0, bulletShootPoint.transform.position.z);
        bullet.directionVector = (GameManager.Instance.playerCharacter.transform.position - shootPointPos).normalized;
        Destroy(bullet.gameObject, 2);
    }

}
