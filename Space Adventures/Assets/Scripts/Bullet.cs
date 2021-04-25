using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int bulletSpeed;
    public Vector3 directionVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(directionVector * bulletSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage();
            gameObject.SetActive(false);
        }
    }

}
