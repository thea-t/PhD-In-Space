using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] AudioSource m_onStopChasingSFX;
    [SerializeField] Bullet m_bulletPrefab;
    [SerializeField] GameObject m_bulletShootPoint;
    [SerializeField] int m_bulletSpeed;

    //In addition to the base of these functions that I've created in the Enemy class, I'm setting the conditions of their animations to true/false 
    //The ranged enemies run while attacking
    //When the enemy's animation reaches to a certain second, there is an animaton event that calls the OnShoot function
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
        m_onStopChasingSFX.PlayDelayed(3);
    }

    //Instantiating a bulley at a specific shootPoint, hidden in the gun of the enemy
    //I'm giving a custom speed of the bullets in the inspector, depending on the levels.
    //Giving the direction of the bullet to be player's position at the moment of shoot which makes it harder to avoid bullets
    //I'm also destroying the bullet in some seconds because I don't want to have unnecessary bullet copies in the scene

    //anim event
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
