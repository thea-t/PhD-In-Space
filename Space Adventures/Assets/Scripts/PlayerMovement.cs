using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creating namespace in order to be able to create classes with a name I want even if the name already exist in the project. As long as the name doesnt exist in my namespace its fine
namespace SpaceAdventures {
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] Rigidbody rb;
        [SerializeField] private int shipSpeed = 20;
        [SerializeField] private float fuel = 20;
        [SerializeField] private float fuelConsumption = 1;
        [SerializeField] float rotSpeed = 40f;
        Vector3 rot = Vector3.zero;
        
        Animator anim;

        MovementState currentMovementState;
        enum MovementState
        {
            player,
            ship
        }
  
        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
            gameObject.transform.eulerAngles = rot;
        }

        private void Start()
        {
            ChangeMovementState(MovementState.ship);
        }

        private void Update()
        {
            if (currentMovementState == MovementState.ship)
            {
                anim.enabled = false;
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(Vector3.forward * shipSpeed);
                    OnFuelOver();
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(Vector3.back * shipSpeed);
                    OnFuelOver();
                }

                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(Vector3.left * shipSpeed);
                    OnFuelOver();
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(Vector3.right * shipSpeed);
                    OnFuelOver();
                }
            }
            else if (currentMovementState == MovementState.player)
            {
                // Walk
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("Walk_Anim", true);
                }
                else if (Input.GetKeyUp(KeyCode.W))
                {
                    anim.SetBool("Walk_Anim", false);
                }

                // Rotate Left
                if (Input.GetKey(KeyCode.A))
                {
                    rot[1] -= rotSpeed * Time.fixedDeltaTime;
                }

                // Rotate Right
                if (Input.GetKey(KeyCode.D))
                {
                    rot[1] += rotSpeed * Time.fixedDeltaTime;
                }
            }
        }
        void OnFuelOver()
        {
            if (fuel < 1)
            {
                shipSpeed = 0;
            }

            else
            {
                fuel -= fuelConsumption * 0.01f;
            }
        }

        void ChangeMovementState(MovementState movementState)
        {
            currentMovementState = movementState;
        }
    }
}
