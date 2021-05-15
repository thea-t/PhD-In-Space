using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GravityController : MonoBehaviour
    {
        [SerializeField] private int m_rotateSpeed;

        public float pullRadius = 2;
        public float pullForce = 1;
        public bool playerIsInRange = false;

        void Update()
        {

            if (playerIsInRange == true)
            {
                //https://answers.unity.com/questions/599949/3d-gravity-towards-one-object.html
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - GameManager.Instance.playerShip.transform.position;

                // apply force on target towards me
                GameManager.Instance.playerShip.rb.AddForce(forceDirection.normalized * pullForce * forceDirection.magnitude, ForceMode.Acceleration);

            }

        if (GameManager.Instance.playerShip.isAllowedToMove == true)
        {
            this.transform.RotateAround(this.transform.parent.position, this.transform.parent.up, m_rotateSpeed * Time.deltaTime);
        }

    }

        

    }

