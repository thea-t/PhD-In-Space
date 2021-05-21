using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeavingAndLandingOnPlanet : MonoBehaviour
{
    [SerializeField] GameObject m_ship;
    [SerializeField] GameObject m_virtualCamera;
    [SerializeField] PlayerCharacter m_playerPrefab;
    [SerializeField] ParticleSystem m_smokeExplosionParticle;
    private Animator m_playerAnimator;
    [SerializeField] AudioSource m_shipIsLandingVFX;
    [SerializeField] AudioSource m_playerLeavingTheShipVFX;
    [SerializeField] AudioSource m_playerEnteringTheShipVFX;
    [SerializeField] AudioSource m_shipIsLeavingVFX;

    private void Start()
    {
        //When the scene loads, players ship moves to 0,0,0 within 3 seconds. The moment this is completed I'm playing sound effect and calling a function
        m_ship.transform.DOMove(new Vector3(0, 0, 0), 3).OnComplete(LandingAnimationStart);
        m_shipIsLandingVFX.Play();
    }
    //Instantiating the playerCharacter and setting its Animator to play the Walk animation while moving to a certain position. Particles and vfx are played as well
    void LandingAnimationStart()
    {
        m_smokeExplosionParticle.Play();
        m_playerLeavingTheShipVFX.Play();
        GameManager.Instance.playerCharacter = Instantiate(m_playerPrefab);

        m_playerAnimator = GameManager.Instance.playerCharacter.GetComponentInChildren<Animator>();
        m_playerAnimator.SetBool("Walk", true);
        GameManager.Instance.playerCharacter.transform.DOMoveZ(3, 2).OnComplete(LandingAnimationEnd);
    }
    //When the player reaches to the point he was supposed to reach to, he is playing an animation and in some more seconds he is allowed to move
    //and his rb is not kinematic anymore. The virtual camera also is set inactive so that the other camera will take control and the scene will have
    //top-down view
    void LandingAnimationEnd()
    {
        m_playerAnimator.SetBool("Walk", false);
        m_playerAnimator.SetTrigger("Look Around");
        m_virtualCamera.SetActive(false);
        StartCoroutine(AllowPlayerToMove());
    }

    IEnumerator AllowPlayerToMove()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.playerCharacter.GetComponent<Rigidbody>().isKinematic = false;
        GameManager.Instance.playerCharacter.canMove = true;
    }
    //Sound and Vfx are played, after that the player's character is destroyed and the virtual camera is set active so that the view will change
    //and look like the player is actually leaving for real, not just changing scenes. Also the ship moves to a certain position and the scene changes 
    public void LeavingAnimation()
    {
        m_playerEnteringTheShipVFX.Play();
        m_smokeExplosionParticle.Play();
        Destroy(GameManager.Instance.playerCharacter.gameObject);
        StartCoroutine(ShipStartsFlyingToSpace());
    }

    IEnumerator ShipStartsFlyingToSpace()
    {
        m_shipIsLeavingVFX.Play();
        yield return new WaitForSeconds(1);
        m_virtualCamera.SetActive(true);
        yield return new WaitForSeconds(2);

        m_ship.transform.DOMove(new Vector3(0, 10, 0), 3).OnComplete(ArriveInGalaxy);
    }

    void ArriveInGalaxy()
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel("Menu");
       // bl_SceneLoaderUtils.GetLoader.LoadLevel((PlayerStats.currentLevel).ToString());
        Debug.Log(PlayerStats.currentLevel.ToString());
    }
}

