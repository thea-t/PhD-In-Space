using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int bulletSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Force);

    }

    //https://forum.unity.com/threads/mouse-position-screen-to-world-coordinates.112708/

}
