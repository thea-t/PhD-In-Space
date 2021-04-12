using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject mainCamera;
        [SerializeField] GameObject followCamera;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("gravity"))
            {
                other.transform.parent.GetComponent<GravityController>().playerIsInRange = true;
            }
            else if (other.CompareTag("planet"))
            {
                //set the parent of the player so that it will move with the planet
                transform.parent = other.transform;
                OnLandingOnPlanet();
            }

        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("gravity"))
            {
                other.transform.parent.GetComponent<GravityController>().playerIsInRange = false;
            }
            
        }

        void OnLandingOnPlanet()
        {
            //change camera
            mainCamera.SetActive(false);
        }
    }
}




