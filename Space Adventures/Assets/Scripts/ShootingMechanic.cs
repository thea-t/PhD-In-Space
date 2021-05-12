using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ShootingMechanic : MonoBehaviour
{
    //https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html

    [SerializeField] Bullet bulletPrefab;
    [SerializeField] GameObject bulletShootPoint;
    [SerializeField] GameObject weapon;
    [SerializeField] Animator playerAnimator;
    Camera mainCam;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckMouseClick();
    }

    //animator event
    void OnShoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletShootPoint.transform.position, transform.rotation);
        bullet.directionVector = transform.forward;
        Destroy(bullet.gameObject, 1.5f);

        ///////////// mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //https://answers.unity.com/questions/1410936/how-to-prevent-a-ui-element-from-clicking-the-game.html
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                weapon.SetActive(true);
                playerAnimator.SetBool("Crossbow Shoot Attack", true);
                GameManager.Instance.playerCharacter.canMove = false;
            }
        }
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
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.SetActive(false);
            playerAnimator.SetBool("Crossbow Shoot Attack", false);
            GameManager.Instance.playerCharacter.canMove = true;
        }
    }

    //switching between left and right mouse click https://riptutorial.com/unity3d/example/12813/read-mouse-button---left--middle--right--clicks


}
