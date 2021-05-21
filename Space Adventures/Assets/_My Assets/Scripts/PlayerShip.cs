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
    [SerializeField] ParticleSystem m_explosion;
    public Rigidbody rb;
    bool m_isMinimapOpen;
    public bool isAllowedToMove;
    bool m_asteroidWarningSFX;
    bool m_explosionPlayed;
    [SerializeField] GameObject [] m_asteroids;
    [SerializeField] GameObject m_shipModel;
    [SerializeField] Image m_asteroidWarning;
    [SerializeField] AudioSource m_shipMovementSFX;
    [SerializeField] AudioSource m_asteroidStormNotificationSFX;
    [SerializeField] AudioSource m_shipExplosionSFX;
    [SerializeField] AudioSource m_asteroidStormSFX;
    [SerializeField] AudioSource m_fireSFX;
    [SerializeField] AudioSource m_fireWarningSFX;

    private void Start()
    {
        //Update all the Ui panels on start and allow player's ship to move
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
    //Move the ship if it is allowed to move by pressing a key on the keyboard. Each key moves the ship in a specific direction by adding force 
    //on its rigidbody and plays and sound effect. It also uses fuel.
    //When the key is released, the particles and sfx stops playing but ship keeps moving due to the previously added force. This is intentional
    //because these are the challenges in space. Its not an easyy task to control a spacecraft
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

    //Using fuel by redusing fuel (fuelShipConsumption) from the fuel of the player. Also updating the Ui the moment fuel is used. 
    //If the fuel is over, asteroid rain starts in a couple of seconds in order to look more realistic
    void UseFuel()
    {
        PlayerStats.playerFuel -= PlayerStats.fuelShipConsumption;
        GameManager.Instance.uiManager.UpdateFuelUi();
        if (PlayerStats.playerFuel <= 0)
        {
            PlayerStats.playerFuel = 0;
            m_shipSpeed = 0;
            StartCoroutine(AsteroidRainDelayed());
        }
    }
    
    //When a storm starts, the first thing that happens is notifying the player by playing SFX and changing the alpha color of the asteroidWarning image
    //After that, the asteroids which are previously put in the scene and set inactive and with true kinematic, start slowly being setActive and 
    //their rigidbodies are not kinematic anymore so that they can fall. Each asteroid is doing this within some random seconds of difference in order to 
    //achieve more natural look of the storm
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

    //Opening the minimap if it not already open when the M button is pressed. When the map opens, evertything on the screen freezes and the 
    //virtual camera switches to an another one
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
   


    void OnTriggerEnter(Collider other)
    {
        //When the player triggers the gravity collider, playerIsInRange is set to true and the planet pulls the player towards it. This logic is 
        //implemented in the GravityController script
        if (other.CompareTag("gravity"))
        {
            other.transform.parent.GetComponent<GravityController>().playerIsInRange = true;
        }
        //When the player collides with  planet, his rigidbody becomes kinematic so that he won't move and the transform is parented to the planet and start scalling
        //The scene changes shortly after that 
        else if (other.CompareTag("planet"))
        {
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 3);
            transform.SetParent(other.transform);

            //how to do something with a delay:
            StartCoroutine(ChangeScene(other.transform.parent.name));
        }

        //When the player triggers the sun collider, which is bigger than the sun itself, sounds are played and cool text is shown
        else if (other.CompareTag("sun"))
        {
            GameManager.Instance.uiManager.ShowCoolTextInSpace("YOU ARE TOO CLOSE TO THE SUN!");
            m_fireWarningSFX.Play();
            m_fireSFX.Play();
        }
        //If the player is being hit by asteroid, its health is set to 0, updated, almost everything is set active and the dead panel is enabled 
       //An explosion is played if it hasnt already where sound effects and particles are being played
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
