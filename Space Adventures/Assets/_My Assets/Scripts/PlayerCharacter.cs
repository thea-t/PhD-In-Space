using LayerLab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : CharacterController
{
    /*[Range(1000, 10000)] */
    [SerializeField] int m_playerSpeed = 4000;
    [SerializeField] Rigidbody m_rb;
    [SerializeField] Transform m_playerModel;
    [SerializeField] Animator m_playerAnimator;
    public bool canMove;

    private void Start()
    {
        // setting the world curve around the players transform
        GameManager.Instance.curvedWorldController.bendPivotPoint = transform;
        GameManager.Instance.uiManager.UpdateHealthUi();
        m_health = PlayerStats.playerHealth;
    }

    void Update()
    {
        CheckKeyForPlayerMovement();
    }

    #region Functions that override CharacterController
    public override void TakeDamage(float damage)
    {
        PlayerStats.playerHealth = m_health;
        base.TakeDamage(damage);
        GameManager.Instance.uiManager.UpdateHealthUi();
    }

    protected override void Die()
    {
        base.Die();
        m_playerAnimator.SetTrigger("Die");
        GameManager.Instance.uiManager.EnableOnDeadPanel();
    }
    #endregion



    void CheckKeyForPlayerMovement()
    {
        if (canMove == true && isDead == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                m_playerModel.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);
                m_rb.AddForce(Vector3.left * m_playerSpeed * Time.deltaTime);
                m_playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                m_playerModel.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);
                m_rb.AddForce(Vector3.right * m_playerSpeed * Time.deltaTime);
                m_playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                m_playerModel.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);
                m_rb.AddForce(Vector3.forward * m_playerSpeed * Time.deltaTime);
                m_playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                m_playerModel.rotation = Quaternion.LookRotation(m_rb.velocity, Vector3.up);
                m_rb.AddForce(Vector3.back * m_playerSpeed * Time.deltaTime);
                m_playerAnimator.SetBool("Run", true);
            }
        }
        //using or operator: https://answers.unity.com/questions/1160817/andor-operator-c.html
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            m_playerAnimator.SetBool("Run", false);
        }

        //ship leaving to space
        if (Input.GetKeyDown(KeyCode.Space) && isDead == false)
        {
            Collider[] touchColliders = Physics.OverlapSphere(transform.position, 2);
            foreach (var touchCollider in touchColliders)
            {
                if (touchCollider.CompareTag("ship"))
                {
                    GameManager.Instance.shipLanding.LeavingAnimation();
                }
            }
        }
    }
}



