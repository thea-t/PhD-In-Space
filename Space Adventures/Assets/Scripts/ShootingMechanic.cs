using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootingMechanic : MonoBehaviour
{
    //https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html

    [SerializeField] Bullet bullet;
    [SerializeField] GameObject bulletShootPoint;
    [SerializeField] Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckMouseClick();
    }

    //animator event
    void OnShoot()
    {
        Instantiate(bullet, bulletShootPoint.transform.position, transform.rotation);

       ///////////// mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerAnimator.SetTrigger("Crossbow Shoot Attack");

        }
    }

    //switching between left and right mouse click https://riptutorial.com/unity3d/example/12813/read-mouse-button---left--middle--right--clicks


}
