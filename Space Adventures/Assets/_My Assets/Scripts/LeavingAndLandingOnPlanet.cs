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

    private void Start()
    {
        m_ship.transform.DOMove(new Vector3(0, 0, 0), 3).OnComplete(LandingAnimationStart);
    }

    void LandingAnimationStart()
    {
        m_smokeExplosionParticle.Play();
        GameManager.Instance.playerCharacter = Instantiate(m_playerPrefab);

        m_playerAnimator = GameManager.Instance.playerCharacter.GetComponentInChildren<Animator>();
        m_playerAnimator.SetBool("Walk", true);
        GameManager.Instance.playerCharacter.transform.DOMoveZ(3, 2).OnComplete(LandingAnimationEnd);
    }

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

    public void LeavingAnimation()
    {
        m_smokeExplosionParticle.Play();
        Destroy(GameManager.Instance.playerCharacter.gameObject);
        StartCoroutine(ShipStartsFlyingToSpace());
    }

    IEnumerator ShipStartsFlyingToSpace()
    {
        yield return new WaitForSeconds(1);
        m_virtualCamera.SetActive(true);
        yield return new WaitForSeconds(2);

        m_ship.transform.DOMove(new Vector3(0, 10, 0), 3).OnComplete(ArriveInGalaxy);
    }

    void ArriveInGalaxy()
    {
        bl_SceneLoaderUtils.GetLoader.LoadLevel((PlayerStats.currentLevel).ToString());
        Debug.Log(PlayerStats.currentLevel.ToString());
    }
}

