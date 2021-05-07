using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] GameObject bulletShootPoint;

    protected override void OnChaseBegin()
    {
        base.OnChaseBegin();
        m_animator.SetBool("Projectile Right Attack 01", true);
        m_animator.SetBool("Run", true);
    }

    protected override void StopChasing()
    {
        m_animator.SetBool("Projectile Right Attack 01", false);
        m_animator.SetBool("Run", false);
        base.StopChasing();
    }

    //animator event
    void OnShoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletShootPoint.transform.position, transform.rotation);
        bullet.directionVector = transform.forward;
        Destroy(bullet.gameObject, 2);
    }

}
