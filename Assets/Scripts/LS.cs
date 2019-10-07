using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 40;
    public GameObject Projectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        var projectile = Instantiate(Projectile, firePoint.position, firePoint.rotation /** Quaternion.Euler(0, 0, 90)*/);
        projectile.AddComponent<Rigidbody>();
        var rigidbody = projectile.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.mass = 0.0001f;
        var r = rigidbody.rotation;
        var v = r * Vector3.down;
        Debug.Log("v: " + v);
        rigidbody.AddForce(v * 100);

        yield return 0;
    }
}