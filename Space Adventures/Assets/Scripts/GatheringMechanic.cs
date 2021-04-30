using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_axePrefab;
    [SerializeField] Animator playerAnimator;
    Camera mainCam;
    Collider axeCollider;

    private void Start()
    {
        axeCollider = m_axePrefab.GetComponent<Collider>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckMouseClick();
    }

    private void OnTriggerEnter(Collider axeCol)
    {
        axeCol = axeCollider;
        if (axeCol.CompareTag("resource"))
        {
            //decide what to do 
        }
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            //https://answers.unity.com/questions/1569674/how-can-i-shoot-a-projectile-on-mouse-position.html
            //https://www.youtube.com/watch?v=-376PylZ5l4&t=335s
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
            float midPoint = (transform.position - mainCam.transform.position).magnitude;

            Vector3 lookAtPosition = mouseRay.origin + mouseRay.direction * midPoint;
            lookAtPosition.y = 0;

            transform.LookAt(lookAtPosition);


            m_axePrefab.SetActive(true);
            playerAnimator.SetBool("Digging", true);
            GameManager.Instance.playerCharacter.canMove = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_axePrefab.SetActive(false);
            playerAnimator.SetBool("Digging", false);
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }

}
