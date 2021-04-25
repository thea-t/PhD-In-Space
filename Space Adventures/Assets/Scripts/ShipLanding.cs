using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class ShipLanding : MonoBehaviour
    {
        [SerializeField] GameObject ship;
        [SerializeField] GameObject landingCamera;
    [SerializeField] PlayerCharacter playerPrefab;
    [SerializeField] ParticleSystem smokeParticleOnPlayerInstantiated;

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
            smokeParticleOnPlayerInstantiated.Play();
            GameManager.Instance.playerCharacter = Instantiate(playerPrefab);

            playerAnimator = GameManager.Instance.playerCharacter.GetComponentInChildren<Animator>();
            playerAnimator.SetBool("Walk", true);
        GameManager.Instance.playerCharacter.transform.DOMoveZ(3, 2).OnComplete(OnLandingAnimationEnd);
        }

        void OnLandingAnimationEnd()
        {
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetTrigger("Look Around");
            landingCamera.SetActive(false);
            StartCoroutine(ChangeScene());
        }

        IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(4);
        GameManager.Instance.playerCharacter.canMove = true;
        GameManager.Instance.playerCharacter.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

