using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool m_isEnemyBullet;
    [HideInInspector] public Vector3 directionVector;
    public int bulletSpeed;
    Rigidbody m_rb;

    //Getting the ridigboby on start in order to save performance.
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //Checking if this bullet is enemy bullet or not. If it is player bullet, I'm setting it to the value that is saved in playerStats
        if (!m_isEnemyBullet)
        {
            bulletSpeed = PlayerStats.bulletSpeed * 100;
        }
    }

    // Addig force to the bullet in a direction and giving it speed. This is done in order to move the bullet after its instantiated
    void Update()
    {
        m_rb.AddForce(directionVector * bulletSpeed * Time.deltaTime, ForceMode.Force);
    }


    //Checking if the bullet is colliding with the enemy or players body. Then calling the TakeDamage function and sending as a parameter how much damage should the hit character take
    //when the bullet collides with any body, it becomes invisible until it gets destroyed. However, the character who instantiates it, destroys it
    private void OnTriggerEnter(Collider other)
    {
        if (!m_isEnemyBullet && other.CompareTag("enemyBody"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(PlayerStats.damageToDeal);
            gameObject.SetActive(false);
        }
        else if (m_isEnemyBullet && other.CompareTag("playerBody"))
        {
            GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToReceiveDamage + PlayerStats.damageToDeal);
            gameObject.SetActive(false);
        }
    }

}
