using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerShip : MonoBehaviour
    {
        [SerializeField] GameObject mainCamera;
        [SerializeField] GameObject followCamera;

        public Rigidbody rb;
        [SerializeField] private int shipSpeed = 20;
        [SerializeField] private float fuel = 20;
        [SerializeField] private float fuelConsumption = 1;
        [SerializeField] float rotSpeed = 40f;
        [SerializeField] ParticleSystem leftSmokeParticle;
        [SerializeField] ParticleSystem rightSmokeParticle;
        [SerializeField] ParticleSystem topSmokeParticle;
        [SerializeField] ParticleSystem botSmokeParticle;


        void Update()
        {
            CheckKeyForShipMovement();
        }

        void UseFuel()
        {
            fuel -= fuelConsumption * 0.01f;

            if (fuel < 1)
            {
                shipSpeed = 0;
            }
        }

        void CheckKeyForShipMovement()
        {
            // UP
            if (Input.GetKeyDown(KeyCode.W))
            {
                botSmokeParticle.Play();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * shipSpeed);
                UseFuel();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                botSmokeParticle.Stop();
            }

            //DOWN
            if (Input.GetKeyDown(KeyCode.S))
            {
                topSmokeParticle.Play();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.back * shipSpeed);
                UseFuel();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                topSmokeParticle.Stop();
            }

            //LEFT
            if (Input.GetKeyDown(KeyCode.A))
            {
                rightSmokeParticle.Play();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left * shipSpeed);
                UseFuel();
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                rightSmokeParticle.Stop();
            }

            //RIGHT
            if (Input.GetKeyDown(KeyCode.D))
            {
                leftSmokeParticle.Play();
            }
            else if (Input.GetKey(KeyCode.D))
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
                transform.localScale = (new Vector3(10,10,10));
                transform.SetParent(other.transform);



                //change camera
                mainCamera.SetActive(false);

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
            yield return new WaitForSeconds(2);
            //how to load a scene with loader asset: check documentation
            bl_SceneLoaderUtils.GetLoader.LoadLevel(_planetName);
        } 
        
        
    }





