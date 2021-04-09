using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class Player : MonoBehaviour
{




     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("planet"))
        {
            other.gameObject.GetComponent<GravityController>().playerIsInRange = true;
        }
        
    } 

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("planet"))
        {
            other.gameObject.GetComponent<GravityController>().playerIsInRange = false;
    }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("planet"))
        {
           
            }
    }

    
    }
    }

