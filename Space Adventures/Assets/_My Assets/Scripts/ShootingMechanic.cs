using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ShootingMechanic : MonoBehaviour
{
    //https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html

    [SerializeField] Bullet m_bulletPrefab;
    [SerializeField] GameObject m_bulletShootPoint;
    [SerializeField] GameObject m_weapon;
    [SerializeField] Animator m_playerAnimator;
    Camera mainCam;

    private void Start()
    {
        m_playerAnimator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckMouseClick();
    }

    //When the animation reaches to the second where the event is set, its calling this function. A bullet is instantiated on the position of the 
    //shoot point which is hidden in the gun, and the bullet is moving in a forward direction. In a couple of seconds this bullet is being destroyed.
    //anim event
    void OnShoot()
    {
        Bullet bullet = Instantiate(m_bulletPrefab, m_bulletShootPoint.transform.position, transform.rotation);
        bullet.directionVector = transform.forward;
        Destroy(bullet.gameObject, 1.5f);

        ///////////// mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void CheckMouseClick()
    {
        //If the mouse button is clicked and is not clicked on top a Ui, the weapon of the player is set active and an attack animation which is 
        //looping is set to true. This attack animation has animation event where when the animatin reaches to a certain second, its calling the OnShoot function
        //Also player's movement is stopped because I don't want player to shoot while running
        if (Input.GetMouseButtonDown(0))
        {
            //https://answers.unity.com/questions/1410936/how-to-prevent-a-ui-element-from-clicking-the-game.html
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                m_weapon.SetActive(true);
                m_playerAnimator.SetBool("Crossbow Shoot Attack", true);
                GameManager.Instance.playerCharacter.canMove = false;
            }
        }
        //If the mouse button is held continuesly, player's character is rotating and looking at the position of the mouse. This is made so that 
        //the player can mouse over enemies and shoot at them
        else if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //https://answers.unity.com/questions/1569674/how-can-i-shoot-a-projectile-on-mouse-position.html
                //https://www.youtube.com/watch?v=-376PylZ5l4&t=335s
                Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
                float midPoint = (transform.position - mainCam.transform.position).magnitude;

                Vector3 lookAtPosition = mouseRay.origin + mouseRay.direction * midPoint;
                lookAtPosition.y = 0;

                transform.LookAt(lookAtPosition);
            }
        }
        //When the mouse button is not clicked anymore, the weapon is becoming inactive and the animation stops playing. Also, player is allowed to move.
        else if (Input.GetMouseButtonUp(0))
        {
            m_weapon.SetActive(false);
            m_playerAnimator.SetBool("Crossbow Shoot Attack", false);
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }
}
