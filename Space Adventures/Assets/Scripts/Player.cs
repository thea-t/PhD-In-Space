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
                OnLanding();
            }

        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("gravity"))
            {
                other.transform.parent.GetComponent<GravityController>().playerIsInRange = false;
            }
            
        }

        void OnLanding()
        {
            //change camera
            mainCamera.SetActive(false);

            //rescale the player
            transform.localScale -= new Vector3(5, 5, 5);
            //change the movement controls
            GameManager.Instance.playerMovement.ChangeMovementState(PlayerMovement.MovementState.player);
        }
    }
}




