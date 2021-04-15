using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] ParticleSystem botSmokeParticle;

    void ShipLandingAnimation()
    {
        ship.transform.DOMove(new Vector3(0, 0, 0), 3);
        botSmokeParticle.Play();
    }
    void ShipFlyingAnimation()
    {
        
    }
}
