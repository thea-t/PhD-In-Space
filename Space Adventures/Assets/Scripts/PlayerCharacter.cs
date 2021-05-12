using LayerLab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : CharacterController
{
    /*[Range(1000, 10000)] */
    [SerializeField] private int playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] Transform playerModel;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject onDeadPanel;
    public bool canMove;

    private void Start()
    {
        // setting the world curve around the players transform
        GameManager.Instance.curvedWorldController.bendPivotPoint = transform;
        m_health = PlayerStats.playerHealth;
        GameManager.Instance.uiManager.UpdateHealthUi();
    }

    void Update()
    {
        CheckKeyForPlayerMovement();
    }

    #region Functions that override CharacterController
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        playerAnimator.SetTrigger("Take Damage");
        PlayerStats.playerHealth = m_health;
        GameManager.Instance.uiManager.UpdateHealthUi();
    }

    protected override void Die()
    {
        base.Die();
        //play animation
        //show panel in 2 seconds
        //play around the camera
        onDeadPanel.SetActive(true);
    }
    #endregion

    void CheckKeyForPlayerMovement()
    {
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                playerModel.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                rb.AddForce(Vector3.left * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                playerModel.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                rb.AddForce(Vector3.right * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                playerModel.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                rb.AddForce(Vector3.forward * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
                playerModel.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                rb.AddForce(Vector3.back * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
            }
        }
        //using or operator: https://answers.unity.com/questions/1160817/andor-operator-c.html
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("Run", false);
        }

        //ship leaving to space
        if (Input.GetKeyDown(KeyCode.Space))
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



