using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool m_isEnemyBullet;
    [HideInInspector] public Vector3 directionVector;

    Rigidbody m_rb;
    float m_bulletSpeed = 500;


    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.AddForce(directionVector * m_bulletSpeed * PlayerStats.multiplierbulletSpeed * Time.deltaTime, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isEnemyBullet && other.CompareTag("enemyBody"))
        {
            other.GetComponentInParent<Enemy>().TakeDamage(PlayerStats.multiplierToDealDamage * PlayerStats.baseDamage);
            gameObject.SetActive(false);
        }
        else if (m_isEnemyBullet && other.CompareTag("playerBody"))
        {
            GameManager.Instance.playerCharacter.TakeDamage(PlayerStats.multiplierToReceiveDamage * PlayerStats.baseDamage);
            gameObject.SetActive(false);
        }
    }

}
