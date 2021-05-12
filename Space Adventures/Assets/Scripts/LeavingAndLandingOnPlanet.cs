using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeavingAndLandingOnPlanet : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject VirtualCamera;
    [SerializeField] PlayerCharacter playerPrefab;
    [SerializeField] ParticleSystem smokeExplosionParticle;
    private Animator playerAnimator;

    private void Start()
    {
        ship.transform.DOMove(new Vector3(0, 0, 0), 3).OnComplete(LandingAnimationStart);
    }

    void LandingAnimationStart()
    {
        smokeExplosionParticle.Play();
        GameManager.Instance.playerCharacter = Instantiate(playerPrefab);

        playerAnimator = GameManager.Instance.playerCharacter.GetComponentInChildren<Animator>();
        playerAnimator.SetBool("Walk", true);
        GameManager.Instance.playerCharacter.transform.DOMoveZ(3, 2).OnComplete(LandingAnimationEnd);
    }

    void LandingAnimationEnd()
    {
        playerAnimator.SetBool("Walk", false);
        playerAnimator.SetTrigger("Look Around");
        VirtualCamera.SetActive(false);
        StartCoroutine(AllowPlayerToMove());
    }

    IEnumerator AllowPlayerToMove()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.playerCharacter.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.Instance.playerCharacter.canMove = true;
    }

    public void LeavingAnimation()
    {
        smokeExplosionParticle.Play();
        Destroy(GameManager.Instance.playerCharacter.gameObject);
        StartCoroutine(ShipStartsFlyingToSpace());
    }

    IEnumerator ShipStartsFlyingToSpace()
    {
        yield return new WaitForSeconds(1);
        VirtualCamera.SetActive(true);
        yield return new WaitForSeconds(2);
        ship.transform.DOMove(new Vector3(0, 10, 0), 3).OnComplete(ArriveInGalaxy);
    }

    void ArriveInGalaxy()
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel(PlayerStats.currentGalaxy);
    }
}

