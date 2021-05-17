using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] int m_shipSpeed = 250;
    [SerializeField] int m_sunDamage = 1;
    [SerializeField] float m_rotSpeed = 40f;
    [SerializeField] GameObject m_followCamera;
    [SerializeField] ParticleSystem m_leftSmokeParticle;
    [SerializeField] ParticleSystem m_rightSmokeParticle;
    [SerializeField] ParticleSystem m_topSmokeParticle;
    [SerializeField] ParticleSystem m_botSmokeParticle;
    public Rigidbody rb;
    bool m_isMinimapOpen;
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
        PlayerStats.playerFuel -= PlayerStats.fuelShipConsumption/1000;
        GameManager.Instance.uiManager.UpdateFuelUi();
        if (PlayerStats.playerFuel < 1)
        {
            m_shipSpeed = 0;
        }
    }

    void MinimapController()
    {
        if (!m_isMinimapOpen && Input.GetKeyDown(KeyCode.M))
        {
            m_followCamera.SetActive(false);
            m_isMinimapOpen = true;
            isAllowedToMove = false;
            rb.isKinematic = true;
        }
        else if (m_isMinimapOpen && Input.GetKeyDown(KeyCode.M))
        {
            m_followCamera.SetActive(true);
            m_isMinimapOpen = false;
            isAllowedToMove = true;
            rb.isKinematic = false;
        }
    }
    void CheckKeyForShipMovement()
    {
        // UP
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.W))
        {
            m_botSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * m_shipSpeed);
            UseFuel();

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            m_botSmokeParticle.Stop();
        }

        //DOWN
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.S))
        {
            m_topSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * m_shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_topSmokeParticle.Stop();
        }

        //LEFT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.A))
        {
            m_rightSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * m_shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_rightSmokeParticle.Stop();
        }

        //RIGHT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.D))
        {
            m_leftSmokeParticle.Play();
        }
        else if (isAllowedToMove && Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * m_shipSpeed);
            UseFuel();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_leftSmokeParticle.Stop();
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
            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 3);
            transform.SetParent(other.transform);

            //how to do something with a delay:
            StartCoroutine(ChangeScene(other.transform.parent.name));
        }
        else if (other.CompareTag("sun"))
        {
            GameManager.Instance.uiManager.ShowCoolTextInSpace("YOU ARE TOO CLOSE TO THE SUN!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("sun"))
        {
            if (PlayerStats.playerHealth > 0)
            {
                PlayerStats.playerHealth -= m_sunDamage * Time.deltaTime;
                GameManager.Instance.uiManager.UpdateHealthUi();
            }
            else
            {
                GameManager.Instance.uiManager.EnableOnDeadPanel();
            }
            Debug.Log(PlayerStats.playerHealth);
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
