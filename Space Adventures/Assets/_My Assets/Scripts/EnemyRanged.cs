using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] Bullet m_bulletPrefab;
    [SerializeField] GameObject m_bulletShootPoint;
    [SerializeField] int m_bulletSpeed;
    [SerializeField] AudioSource m_onStopChasingSFX;

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
        m_onStopChasingSFX.PlayDelayed(5);
    }

    //animator event
    void OnShoot()
    {
        if (!GameManager.Instance.playerCharacter.isDead && !isDead)
        {
            Bullet bullet = Instantiate(m_bulletPrefab, m_bulletShootPoint.transform.position, transform.rotation);
            bullet.bulletSpeed = m_bulletSpeed*100;
            Vector3 shootPointPos = new Vector3(m_bulletShootPoint.transform.position.x, 0, m_bulletShootPoint.transform.position.z);
            bullet.directionVector = (GameManager.Instance.playerCharacter.transform.position - shootPointPos).normalized;
            Destroy(bullet.gameObject, 2);
        }
    }

}
