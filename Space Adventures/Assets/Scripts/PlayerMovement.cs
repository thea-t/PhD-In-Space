using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creating namespace in order to be able to create classes with a name I want even if the name already exist in the project. As long as the name doesnt exist in my namespace its fine
namespace SpaceAdventures
{
    public class PlayerMovement : MonoBehaviour
    {

        public Rigidbody rb;
        [SerializeField] private int shipSpeed = 20;
        [SerializeField] private float fuel = 20;
        [SerializeField] private float fuelConsumption = 1;
        [SerializeField] float rotSpeed = 40f;

        private void Update()
        {
            CheckKey();
        }

        void UseFuel()
        {
            fuel -= fuelConsumption * 0.01f;

            if (fuel < 1)
            {
                shipSpeed = 0;
            }
        }

        void CheckKey()
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * shipSpeed);
                UseFuel();
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.back * shipSpeed);
                UseFuel();
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left * shipSpeed);
                UseFuel();
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector3.right * shipSpeed);
                UseFuel();
            }
            
        }


    }
}
