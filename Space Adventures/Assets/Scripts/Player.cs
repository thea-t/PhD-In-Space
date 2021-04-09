using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float fuel = 20;
    [SerializeField] private float fuelConsumption = 1;
    [SerializeField] private int shipSpeed = 20;

    void Update()
    {
       

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

    void OnFuelOver()
    {
        if (fuel<1)
        {
            shipSpeed = 0;
        }

        else
        {
            fuel -= fuelConsumption * 0.01f;
        }
    }
    }

