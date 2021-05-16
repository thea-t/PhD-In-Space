using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatheringMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_axePrefab;
    [SerializeField] Animator m_playerAnimator;
    Camera m_mainCam;

    private void Start()
    {
        m_mainCam = Camera.main;
    }

    private void Update()
    {
        CheckMouseClick();
        CollectSample();
    }
    
    //anim evet
    void OnFuelGathered()
    {
        //https://forum.unity.com/threads/physics-overlapsphere.476277/
        Collider[] hitColliders = Physics.OverlapSphere(transform.parent.position, 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("fuel"))
            {
                hitCollider.GetComponent<Fuel>().StartGathering();
            }
        }

    }
    void CollectSample()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] touchColliders = Physics.OverlapSphere(transform.parent.position, 1);
            foreach (var touchCollider in touchColliders)
            {
                if (touchCollider.CompareTag("alienSample"))
                {
                    Destroy(touchCollider.gameObject);
                    OnSampleCollected();
                }
            }
        }
        
    }

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
            GameManager.Instance.uiManager.ShowNextLevelUnlockedNotification();
            PlayerStats.dnaSampleCount = 0;
        }
        
        Debug.Log("dna COUNT: " + PlayerStats.dnaSampleCount);
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_axePrefab.SetActive(true);
            m_playerAnimator.SetBool("Digging", true);
            GameManager.Instance.playerCharacter.canMove = false;
        }
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
        else if (Input.GetMouseButtonUp(1))
        {
            m_axePrefab.SetActive(false);
            m_playerAnimator.SetBool("Digging", false);
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }

}
