using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce;
    void FixedUpdate()
    {
        
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, thrustForce * Time.deltaTime, 0);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, -thrustForce * Time.deltaTime, 0);
        }
    }
}