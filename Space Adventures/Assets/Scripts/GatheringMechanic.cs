using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_axePrefab;
    [SerializeField] Animator playerAnimator;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
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
                    PlayerStats.dnaSampleCount++;

                    Debug.Log("samples collected: " + PlayerStats.dnaSampleCount);
                }
            }
        }
        
    }
    /// <summary>
    /// /////////////////////////////////////CALLING COLLECT SAMPLE ON UPDATE AND COLLECTING 3SAMPLES AT ONCE INSTEAD OF 2
    /// </summary>

    void CheckMouseClick()
    {
        if (Input.GetMouseButton(1))
        {
            GameManager.Instance.playerCharacter.canMove = false;

            //https://answers.unity.com/questions/1569674/how-can-i-shoot-a-projectile-on-mouse-position.html
            //https://www.youtube.com/watch?v=-376PylZ5l4&t=335s
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
            float midPoint = (transform.position - mainCam.transform.position).magnitude;

            Vector3 lookAtPosition = mouseRay.origin + mouseRay.direction * midPoint;
            lookAtPosition.y = 0;

            transform.LookAt(lookAtPosition);


            m_axePrefab.SetActive(true);
            playerAnimator.SetBool("Digging", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            m_axePrefab.SetActive(false);
            playerAnimator.SetBool("Digging", false);
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }

}
