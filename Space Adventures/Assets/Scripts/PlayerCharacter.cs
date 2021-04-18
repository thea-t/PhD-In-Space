using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{

    public class PlayerCharacter : MonoBehaviour
    {
        [Range(1, 10)][SerializeField] private int playerSpeed;
        [SerializeField] private Rigidbody rb;
        private void Start()
        {
            GameManager.Instance.curvedWorldController.bendPivotPoint = transform;
        }

        void Update()
        {
            CheckKeyForPlayerMovement();
        }

        void CheckKeyForPlayerMovement()
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left * playerSpeed*100);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector3.right * playerSpeed*100);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * playerSpeed*100);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.back * playerSpeed*100);
            }
        }

    }
}

