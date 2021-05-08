using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] int shipSpeed = 20;
    [SerializeField] float fuel = 20;
    [SerializeField] float fuelConsumption = 1;
    [SerializeField] float rotSpeed = 40f;
    [SerializeField] GameObject followCamera;
    [SerializeField] ParticleSystem leftSmokeParticle;
    [SerializeField] ParticleSystem rightSmokeParticle;
    [SerializeField] ParticleSystem topSmokeParticle;
    [SerializeField] ParticleSystem botSmokeParticle;
    public Rigidbody rb;
    bool isMinimapOpen;
    public bool isAllowedToMove;

    private void Start()
    {
        isAllowedToMove = true;
    }
    void Update()
    {
        CheckKeyForShipMovement();
        MinimapController();
    }

    void UseFuel()
    {
        fuel -= fuelConsumption * 0.01f;

        if (fuel < 1)
        {
            shipSpeed = 0;
        }
    }

    void MinimapController()
    {
        if (!isMinimapOpen && Input.GetKeyDown(KeyCode.M))
        {
            followCamera.SetActive(false);
            isMinimapOpen = true;
            isAllowedToMove = false;
            rb.isKinematic = true;
        }
        else if (isMinimapOpen && Input.GetKeyDown(KeyCode.M))
        {
            followCamera.SetActive(true);
            isMinimapOpen = false;
            isAllowedToMove = true;
            rb.isKinematic = false;
        }
    }
    void CheckKeyForShipMovement()
    {
        // UP
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.W))
        {
            botSmokeParticle.Play();
        }
        else if (isAllowedToMove&&Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * shipSpeed);
            UseFuel();
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            botSmokeParticle.Stop();
        }

        //DOWN
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.S))
        {
            topSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            topSmokeParticle.Stop();
        }

        //LEFT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.A))
        {
            rightSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rightSmokeParticle.Stop();
        }

        //RIGHT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.D))
        {
            leftSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            leftSmokeParticle.Stop();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gravity"))
        {
            other.transform.parent.GetComponent<GravityController>().playerIsInRange = true;
        }
        else if (other.CompareTag("planet"))
        {

            rb.isKinematic = true;
            transform.localScale = (new Vector3(10, 10, 10));
            transform.SetParent(other.transform);

            //how to do something with a delay:
            StartCoroutine(ChangeScene(other.transform.parent.name));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("gravity"))
        {
            other.transform.parent.GetComponent<GravityController>().playerIsInRange = false;
        }

    }

    IEnumerator ChangeScene(string _planetName)
    {
        yield return new WaitForSeconds(1);
        //how to load a scene with loader asset: check documentation
        bl_SceneLoaderUtils.GetLoader.LoadLevel(_planetName);
    }


}





