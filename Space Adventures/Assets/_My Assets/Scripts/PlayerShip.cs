using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] int m_shipSpeed = 250;
    [SerializeField] int m_sunDamage = 10;
    [SerializeField] float m_rotSpeed = 40f;
    [SerializeField] GameObject m_followCamera;
    [SerializeField] ParticleSystem m_leftSmokeParticle;
    [SerializeField] ParticleSystem m_rightSmokeParticle;
    [SerializeField] ParticleSystem m_topSmokeParticle;
    [SerializeField] ParticleSystem m_botSmokeParticle;
    public Rigidbody rb;
    bool m_isMinimapOpen;
    public bool isAllowedToMove;
    bool m_asteroidWarningSFX;
    bool m_explosionPlayed;
    [SerializeField] GameObject [] m_asteroids;
    [SerializeField] GameObject m_shipModel;
    [SerializeField] Image m_asteroidWarning;
    [SerializeField] ParticleSystem m_explosion;
    [SerializeField] AudioSource m_shipMovementSFX;
    [SerializeField] AudioSource m_asteroidStormNotificationSFX;
    [SerializeField] AudioSource m_shipExplosionSFX;
    [SerializeField] AudioSource m_asteroidStormSFX;
    [SerializeField] AudioSource m_fireSFX;
    [SerializeField] AudioSource m_fireWarningSFX;

    private void Start()
    {
        GameManager.Instance.uiManager.UpdateHealthUi();
        GameManager.Instance.uiManager.UpdateDnaSamplesBarUi();
        GameManager.Instance.uiManager.UpdateFuelUi();
        isAllowedToMove = true;
    }
    void Update()
    {
        CheckKeyForShipMovement();
        MinimapController();
    }

    IEnumerator AsteroidRainDelayed()
    {
        yield return new WaitForSeconds(2);

        if (!m_asteroidWarningSFX)
        {
            m_asteroidStormNotificationSFX.Play();
            m_asteroidStormSFX.PlayDelayed(2);
            m_asteroidWarningSFX = true;
            m_asteroidWarning.DOFade(1, 1);
        }

        for (int i = 0; i < m_asteroids.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3));
            m_asteroids[i].SetActive(true);
            m_asteroids[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void UseFuel()
    {
        PlayerStats.playerFuel -= (float)PlayerStats.fuelShipConsumption/10;
        GameManager.Instance.uiManager.UpdateFuelUi();
        if (PlayerStats.playerFuel <= 0)
        {
            PlayerStats.playerFuel = 0;
            m_shipSpeed = 0;
            StartCoroutine(AsteroidRainDelayed());
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
            m_shipMovementSFX.Play();
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
            m_shipMovementSFX.Stop();
        }

        //DOWN
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.S))
        {
            m_shipMovementSFX.Play();
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
            m_shipMovementSFX.Stop();
        }

        //LEFT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.A))
        {
            m_shipMovementSFX.Play();
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
            m_shipMovementSFX.Stop();
        }

        //RIGHT
        if (isAllowedToMove && Input.GetKeyDown(KeyCode.D))
        {
            m_shipMovementSFX.Play();
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
            m_shipMovementSFX.Stop();
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
            GameManager.Instance.uiManager.UpdateHealthUi();
            m_fireWarningSFX.Play();
            m_fireSFX.Play();
        }
        else if (other.CompareTag("asteroid"))
        {
            PlayerStats.playerHealth = 0;
            GameManager.Instance.uiManager.UpdateHealthUi();
            GameManager.Instance.uiManager.EnableOnDeadPanel();
            m_asteroids[m_asteroids.Length - 1].SetActive(false);
            m_shipModel.SetActive(false);
            isAllowedToMove = false;

            if (!m_explosionPlayed)
            {
                m_asteroidStormSFX.Stop();
                m_explosion.Play();
                m_shipExplosionSFX.Play();
                m_explosionPlayed = true;
            }
           
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
                rb.isKinematic = true;
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
        else if(other.CompareTag("sun"))
        {
            m_fireWarningSFX.Stop();
            m_fireSFX.Stop();
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
