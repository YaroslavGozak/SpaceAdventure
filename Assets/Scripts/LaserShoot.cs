using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 40;
    public GameObject Projectile;
    private Ship _ship = Ship.Instance;

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
        var rig = projectile.GetComponent<Rigidbody>();
        rig.useGravity = false;
        rig.mass = 0.001f;
        var r = rig.rotation;
        var v = r * Vector3.down;
        Debug.Log("v: " + v * -1);
        rig.AddForce(v * -1);
        _ship.SubstracEnergy(5);

        yield return 0;
    }
}