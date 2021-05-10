using LayerLab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : MonoBehaviour
{
    /*[Range(1000, 10000)] */
    [SerializeField] private int playerSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] Transform playerModel;
    [SerializeField] Animator playerAnimator;
    [SerializeField] ParticleSystem onShotParticle;
    [SerializeField] ParticleSystem onDeadParticle;
    [SerializeField] Panel onDeadPanel;
    public bool canMove;


    private void Start()
    {
        // setting the world curve around the players transform
        GameManager.Instance.curvedWorldController.bendPivotPoint = transform;
    }

    void Update()
    {
        CheckKeyForPlayerMovement();
    }

    void CheckKeyForPlayerMovement()
    {
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
                RotateCharacter();
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnimator.SetBool("Run", false);
                //canMove = false;
            }


            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector3.right * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
                RotateCharacter();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnimator.SetBool("Run", false);
            }


            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
                RotateCharacter();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnimator.SetBool("Run", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.back * playerSpeed * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
                RotateCharacter();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnimator.SetBool("Run", false);
            }
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

    void RotateCharacter()
    {
        if (rb.velocity != Vector3.zero)
        {
            //https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
            playerModel.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        }
    }


    public void TakeDamage()
    {
        PlayerStats.playerHealth -= PlayerStats.playerMultiplierToReceiveDamage;
        playerAnimator.SetTrigger("Take Damage");
        ParticleSystem particle = Instantiate(onShotParticle, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 2);

        Debug.Log(PlayerStats.playerHealth);

        if (PlayerStats.playerHealth <= 0)
        {
            Debug.Log("DEAD");
            //play animation
            //show panel in 2 seconds
            //play around the camera
            onDeadPanel.gameObject.SetActive(true);
        }
    }



}



