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
        //When the player is in the range of the planet(when he triggers its collider which acts like a graviational field) , it is being pulled towards the planet
            if (playerIsInRange == true)
            {
                //https://answers.unity.com/questions/599949/3d-gravity-towards-one-object.html
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - GameManager.Instance.playerShip.transform.position;

                // apply force on target towards me
                GameManager.Instance.playerShip.rb.AddForce(forceDirection.normalized * pullForce * forceDirection.magnitude, ForceMode.Acceleration);

            }
            // Rotate the planets around their parents position (0,0,0) and create an illusion of them sinning around the sun, just like in space
            //I'm doing this only if they are allowed to move, which is all the time exept from when the player is checking the map
        if (GameManager.Instance.playerShip.isAllowedToMove == true)
        {
            this.transform.RotateAround(this.transform.parent.position, this.transform.parent.up, m_rotateSpeed * Time.deltaTime);
        }
    }

    }

