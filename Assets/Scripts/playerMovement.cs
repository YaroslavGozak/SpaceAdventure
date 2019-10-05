using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce, x_edge = 9, y_edge = 5, k = 0.01f;
    public Vector3 pl_pos, movement;

    void FixedUpdate()
    {

        //up
        if (Input.GetKey("w") && validate_pos())
        {
            var newPosition = new Vector3(rb.position.x, rb.position.y + k * thrustForce * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
        //down
        if (Input.GetKey("s") && validate_pos())
        {
            var newPosition = new Vector3(rb.position.x, rb.position.y - k * thrustForce * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
        //left
        if (Input.GetKey("d") && validate_pos())
        {
            var newPosition = new Vector3(rb.position.x + k * thrustForce * Time.deltaTime, rb.position.y);
            rb.MovePosition(newPosition);
        }
        //right
        if (Input.GetKey("a") && validate_pos())
        {
            var newPosition = new Vector3(rb.position.x - k * thrustForce * Time.deltaTime, rb.position.y);
            rb.MovePosition(newPosition);
        }
        //rotate to top
        if (Input.GetKey("q") && validate_pos())
        {
            rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(k * thrustForce, Vector3.left));
        }
        //rotate to bottom
        if (Input.GetKey("e") && validate_pos())
        {
            rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(-k * thrustForce, Vector3.left));
        }
    }

    private bool validate_pos()
    {
        if (rb.transform.position.y < y_edge && rb.transform.position.y > -y_edge && rb.transform.position.x < x_edge && rb.transform.position.x > -x_edge)
        {
            return true;
        }
        else
        {
            if (rb.transform.position.y > y_edge)
            {
                rb.MovePosition(transform.position - k * thrustForce * transform.up * Time.deltaTime);
            }
            else if (rb.transform.position.y < -y_edge)
            {
                rb.MovePosition(transform.position + k * thrustForce * transform.up * Time.deltaTime);
            }
            else if (rb.transform.position.x < -x_edge)
            {
                rb.MovePosition(transform.position + k * thrustForce * transform.forward * Time.deltaTime);
            }
            else if (rb.transform.position.x > x_edge)
            {
                rb.MovePosition(transform.position - k * thrustForce * transform.forward * Time.deltaTime);
            }
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided: " + other.tag);
        // Change the cube color to green.
        MeshRenderer meshRend = GetComponent<MeshRenderer>();
    }

}