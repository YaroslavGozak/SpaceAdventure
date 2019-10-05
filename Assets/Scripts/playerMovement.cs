using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce;

    private float deltaPosition = 5F;
    void FixedUpdate()
    {
        
        if (Input.GetKey("w"))
        {
            var newPosition = new Vector3(rb.position.x, rb.position.y + deltaPosition * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
        if (Input.GetKey("s"))
        {
            var newPosition = new Vector3(rb.position.x, rb.position.y - deltaPosition * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }
}