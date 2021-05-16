using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool m_isEnemyBullet;
    [HideInInspector] public Vector3 directionVector;
    public float bulletSpeed;
    Rigidbody m_rb;


    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        if (!m_isEnemyBullet)
        {
            bulletSpeed = PlayerStats.bulletSpeed*100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.AddForce(directionVector * bulletSpeed * Time.deltaTime, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isEnemyBullet && other.CompareTag("enemyBody"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(PlayerStats.multiplierToDealDamage + PlayerStats.baseDamage);
            gameObject.SetActive(false);
        }
        else if (m_isEnemyBullet && other.CompareTag("playerBody"))
        {
            GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToReceiveDamage + PlayerStats.baseDamage);
            gameObject.SetActive(false);
        }
    }

}
