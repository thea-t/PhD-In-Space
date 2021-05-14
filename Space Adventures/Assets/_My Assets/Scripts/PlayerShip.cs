using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] int shipSpeed = 20;
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
        PlayerStats.playerFuel -= PlayerStats.fuelShipConsumption;
        GameManager.Instance.uiManager.UpdateFuelUi();
        if (PlayerStats.playerFuel < 1)
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
            GetComponent<Collider>().enabled = false;
            transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 3);
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
        Debug.Log(_planetName);
        yield return new WaitForSeconds(3);
        //how to load a scene with loader asset: check documentation
        bl_SceneLoaderUtils.GetLoader.LoadLevel(_planetName);
    }


}





