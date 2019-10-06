using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageble = collision.gameObject.GetComponent<Damageble>();
        if(damageble != null)
        {
            
            damageble.TakeDamage(Damage);
            Debug.Log($"{damageble.name} : {damageble.health}");
            Destroy(gameObject);
        }
    }
}
