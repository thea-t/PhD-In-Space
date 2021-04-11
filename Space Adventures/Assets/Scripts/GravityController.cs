using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class GravityController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    public float pullRadius = 2;
    public float pullForce = 1;
    public bool playerIsInRange = false;

    void Update()
    {
        if (playerIsInRange == true)
        {
            //https://answers.unity.com/questions/599949/3d-gravity-towards-one-object.html
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - GameManager.Instance.player.transform.position;

            // apply force on target towards me
            GameManager.Instance.player.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * forceDirection.magnitude, ForceMode.Acceleration);
        }

        this.transform.RotateAround(this.transform.parent.position, this.transform.parent.up, rotateSpeed * Time.deltaTime);

    }

    private void OnGUI()
    {
        Vector3 forceDirection = transform.position - GameManager.Instance.player.transform.position;
        Debug.DrawLine(transform.position, forceDirection);
    }

}
}
