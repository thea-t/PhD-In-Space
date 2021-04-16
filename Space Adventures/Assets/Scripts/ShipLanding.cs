using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class ShipLanding : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject landingCamera;

    private GameObject player;
    private Animator playerAnimator;

    private void Start()
    {
        ShipLandingAnimation();
    }

    void ShipLandingAnimation()
    {
        ship.transform.DOMove(new Vector3(0, 0, 0), 3).OnComplete(OnLandingAnimationStart);
    }

    void OnLandingAnimationStart()
    {
        player = Instantiate(playerPrefab);
        playerAnimator = player.GetComponentInChildren<Animator>();

        playerAnimator.SetBool("Walk", true);
        player.transform.DOMoveZ(3, 2).OnComplete(OnLandingAnimationEnd);
    }

    void OnLandingAnimationEnd()
    {
        playerAnimator.SetBool("Walk", false);
        playerAnimator.SetTrigger("Look Around");
        landingCamera.SetActive(false);
    }

}
}
