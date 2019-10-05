using Uitry;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce, x_edge, y_edge, k;
    public Vector3 pl_pos, movement;

    private int _maxSpeed = 3;
    private Ship _ship;

    private void Start()
    {
        _ship = Ship.Instance;
    }
    void FixedUpdate()
    {
        validate_pos();
        //up
        if (Input.GetKey("w") && validate_pos() && rb.velocity.y < _maxSpeed)
        {
            var newPosition = Vector3.up * k * thrustForce;
            MoveShip(newPosition);
        }
        //down
        if (Input.GetKey("s") && validate_pos() && rb.velocity.y > -_maxSpeed)
        {
            var newPosition = Vector3.down * k * thrustForce;
            MoveShip(newPosition);
        }
        //left
        if (Input.GetKey("d") && validate_pos() && rb.velocity.x < _maxSpeed)
        {
            var newPosition = Vector3.right * k * thrustForce;
            MoveShip(newPosition);
        }
        //right
        if (Input.GetKey("a") && validate_pos() && rb.velocity.x > -_maxSpeed)
        {
            var newPosition = Vector3.left * k * thrustForce;
            MoveShip(newPosition);
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
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.down);
            }
            else if (rb.transform.position.y < -y_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up);
            }
            else if (rb.transform.position.x < -x_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.right);
            }
            else if (rb.transform.position.x > x_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.left);
            }
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided: " + other.tag);
    }

    private void MoveShip(Vector3 newPosition)
    {
        rb.AddForce(newPosition);
        _ship.SubstracEnergy(1);
        Debug.Log("ENERGY: " + _ship.Energy);
    }

}