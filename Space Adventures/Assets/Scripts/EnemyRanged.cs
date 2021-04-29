using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] GameObject bulletShootPoint;

    protected override void Attack()
    {
        m_animationOnAttack = "Projectile Right Attack 01";

        base.Attack();

    }


    //animator event
    void OnShoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletShootPoint.transform.position, transform.rotation);
        bullet.directionVector = transform.forward;
        Destroy(bullet.gameObject, 3);
    }
}
