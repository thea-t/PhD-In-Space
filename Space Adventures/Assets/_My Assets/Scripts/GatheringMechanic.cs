using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatheringMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_axePrefab;
    [SerializeField] Animator m_playerAnimator;
    [SerializeField] AudioSource m_collectionSFX;
    [SerializeField] AudioSource m_gatheringSFX;
    Camera m_mainCam;
    int m_PowerUpHealth = 20;
    int m_PowerUpMaxHealth = 5;
    int m_PowerUpMaxFuel = 5;
    int m_PowerUpStrength = 1;
    int m_PowerUpBulletSpeed = 1;
    float m_PowerUpfuelShipConsumption = 0.01f;


    enum PowerUp
    {
        GainHealth,
        GainMaxHealth,
        GainMaxFuel,
        GainStrength,
        FasterBulletSpeed,
        ReduceFuelConsumption
    }


    private void Start()
    {
        //Setting the m_main cam to the main camera because I will use that later on to make my player rotate on the position of the screen of my mouse when I click
        m_mainCam = Camera.main;
        GameManager.Instance.uiManager.UpdateFuelUi();
    }

    private void Update()
    {
        CheckMouseClick();
        CollectSample();
    }
    
    //This is an animation event called when the gathering animation reaches to a certain second. It checks all the colliders near me in a certain 
    //radius and if these colliders have a tag "fuel", sound effect is played and I'm calling the startGathering function located in the Fuel class
    //anim evet
    void OnFuelGathered()
    {
        
        //https://forum.unity.com/threads/physics-overlapsphere.476277/
        Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("fuel"))
            {
                m_gatheringSFX.Play();
                hitCollider.GetComponent<Fuel>().StartGathering();
            }
        }

    }

    //If the space button is pressed it checks all the colliders near me in a certain 
    //radius and if these colliders have a tag "alienSample", it destroys the sample and collects it by calling the OnSampleCollected function
    //if the tag is different, it calls different function(powerUp for example)
    void CollectSample()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Collider[] touchColliders = Physics.OverlapSphere(transform.parent.position, 1);
            foreach (var touchCollider in touchColliders)
            {
                m_collectionSFX.Play();
                if (touchCollider.CompareTag("alienSample"))
                {
                    Destroy(touchCollider.gameObject);
                    OnSampleCollected();
                }
                else if (touchCollider.CompareTag("powerUp"))
                {
                    Destroy(touchCollider.gameObject);
                    Debug.Log("power up collected") ;
                    OnPowerUpCollected();
                }
            }
        }
        
    }
    void OnPowerUpCollected()
    {
        //Picking a random int between 0 and the lenght of the Enum
        //Depending on the number that has been picked, I'm increasing certain stats , updating Ui and showing notifications
        //https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
        int random = Random.Range(0, PowerUp.GetNames(typeof(PowerUp)).Length);

        switch (random)
        {
            case (int)PowerUp.GainHealth:
                if (PlayerStats.playerHealth <= PlayerStats.maxHealth - m_PowerUpHealth)
                {
                    PlayerStats.playerHealth += m_PowerUpHealth;
                }
                GameManager.Instance.uiManager.ShowNotificationText("Health gained!");
                GameManager.Instance.uiManager.UpdateHealthUi();
                Debug.Log("+health: " + PlayerStats.playerHealth);
                break;

            case (int)PowerUp.GainMaxHealth:
                PlayerStats.maxHealth += m_PowerUpMaxHealth; 
                GameManager.Instance.uiManager.ShowNotificationText("Max health gained!");
                GameManager.Instance.uiManager.UpdateHealthUi();
                Debug.Log("+ max health: " + PlayerStats.maxHealth);
                break;
            case (int)PowerUp.GainMaxFuel:
                PlayerStats.maxFuel += m_PowerUpMaxFuel;
                GameManager.Instance.uiManager.ShowNotificationText("Max fuel gained!");
                GameManager.Instance.uiManager.UpdateFuelUi();
                Debug.Log("+ max fuel: " + PlayerStats.maxFuel);
                break;

            case (int)PowerUp.GainStrength:
                PlayerStats.damageToDeal += m_PowerUpStrength;
                GameManager.Instance.uiManager.ShowNotificationText("Strength gained!");
                Debug.Log("+ strenght:" + PlayerStats.damageToDeal);
                break;

            case (int)PowerUp.FasterBulletSpeed:
                PlayerStats.bulletSpeed += m_PowerUpBulletSpeed;
                GameManager.Instance.uiManager.ShowNotificationText("Weapon is upgraded");
                Debug.Log("+ fster bulltet:" + PlayerStats.bulletSpeed);
                break;

            case (int)PowerUp.ReduceFuelConsumption:
                PlayerStats.fuelShipConsumption-=m_PowerUpfuelShipConsumption;
                GameManager.Instance.uiManager.ShowNotificationText("Ship is consuming less fuel!");
                Debug.Log("+ consume less fuel:" + PlayerStats.fuelShipConsumption);
                break;
        }

    }
    //When a sample is collected i'm increasing the dnaSampleCount by one and adding the name of the current planet to the list of completedPlanets
    //When there are 3 samples collected I'm unlocking the next level and saving it in playerPrefs. I'm also showing a notification that the 
    //next level is unlocked and I'm setting the dnaSampleCount to 0
    void OnSampleCollected()
    {
        PlayerStats.dnaSampleCount++;
        //https://docs.unity3d.com/ScriptReference/SceneManagement.Scene-name.html
        PlayerStats.completedPlanets.Add(SceneManager.GetActiveScene().name);
        GameManager.Instance.uiManager.UpdateDnaSamplesBarUi();

        if (PlayerStats.dnaSampleCount == 3)
        {
            PlayerStats.currentLevel = PlayerStats.currentLevel+1;

            PlayerPrefs.SetInt("currentLevel", (int)PlayerStats.currentLevel);
            GameManager.Instance.uiManager.ShowNotificationText(("Galaxy " + PlayerStats.currentLevel.ToString() + " is unlocked!"));
            PlayerStats.dnaSampleCount = 0;
        }
        
        Debug.Log("dna COUNT: " + PlayerStats.dnaSampleCount);
    }
    //If the mouse button is clicked the axe of the player is set active and an digging animation which is 
    //looping is set to true. This digging animation has animation event where when the animatin reaches to a certain second, its calling the OnGather function
    //Also player's movement is stopped because I don't want player to shoot while running
    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_axePrefab.SetActive(true);
            m_playerAnimator.SetBool("Digging", true);
            GameManager.Instance.playerCharacter.canMove = false;
        }
        //If the mouse button is held continuesly, player's character is rotating and looking at the position of the mouse. 
        else if (Input.GetMouseButton(1))
        {
            //https://answers.unity.com/questions/1569674/how-can-i-shoot-a-projectile-on-mouse-position.html
            //https://www.youtube.com/watch?v=-376PylZ5l4&t=335s
            Ray mouseRay = m_mainCam.ScreenPointToRay(Input.mousePosition);
            float midPoint = (transform.position - m_mainCam.transform.position).magnitude;

            Vector3 lookAtPosition = mouseRay.origin + mouseRay.direction * midPoint;
            lookAtPosition.y = 0;

            transform.LookAt(lookAtPosition);
        }
        //When the mouse button is released, the axe is becoming inactive and the animation stops playing. Also, player is allowed to move.
        else if (Input.GetMouseButtonUp(1))
        {
            m_axePrefab.SetActive(false);
            m_playerAnimator.SetBool("Digging", false);
            m_gatheringSFX.Stop();
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }

}
