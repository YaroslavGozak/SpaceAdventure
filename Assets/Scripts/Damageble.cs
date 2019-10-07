using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uitry;

public class Damageble : MonoBehaviour
{

    public int health = 100;

    public GameObject deathEffect;

    private Ship _ship = Ship.Instance;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (gameObject.name == "SpaceTrash(Clone)")
        {
            _ship.IncreaseScore();
        }
       

        Destroy(gameObject);
    }

}