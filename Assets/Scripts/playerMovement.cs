using System;
using System.Collections.Generic;
using System.Linq;
using Uitry;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce, x_edge, y_edge, k;
    public Vector3 pl_pos, movement;

    private int _maxSpeed = 3;
    private Ship _ship;
    private Dictionary<IModule, GameObject> _moduleObjects;

    private void Start()
    {
        _ship = Ship.Instance;
        _moduleObjects = new Dictionary<IModule, GameObject>();
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
        Debug.Log($"Collided with {other.gameObject.tag}");
        if (other.gameObject.tag == "solar_panel")
        {
            //Debug.Log("Got Solar Panel");
            //other.gameObject.transform.SetParent(gameObject.transform);
            //Debug.Log("other.gameObject.transform.parent" + other.gameObject.transform.parent.name);
            //other.gameObject.transform.localPosition = gameObject.GetComponent<PositionReferences>().GetNextPosition();
            //Debug.Log("other.gameObject.transform.localPosition" + other.gameObject.transform.localPosition);

            //var hingeJoint = new HingeJoint();
            //hingeJoint = gameObject.AddComponent<HingeJoint>();
            //var otherRigidBody = other.GetComponent<Rigidbody>();
            //hingeJoint.connectedBody = other.GetComponent<Rigidbody>();
            //otherRigidBody.mass = 0.00001F;
            //otherRigidBody.freezeRotation = true;
            //otherRigidBody.velocity = new Vector3(0, 0, 0);
            //otherRigidBody.rotation = new Quaternion(0, 0, 0, 0);
            //GetComponent<Collider>().material.bounciness = 0;

            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.anchor = gameObject.GetComponent<PositionReferences>().GetNextPosition();
            fixedJoint.connectedBody = other.GetComponent<Rigidbody>();
            fixedJoint.transform.position = fixedJoint.anchor;
            fixedJoint.transform.rotation = Quaternion.identity;

            var otherRigidBody = other.GetComponent<Rigidbody>();
            otherRigidBody.mass = 0.00001F;

            var otherCollider = other.GetComponent<Collider>();
            otherCollider.enabled = false;

            var module = new SolarPanelModule();
            _moduleObjects.Add(module, other.gameObject);
            _ship.AddModule(module);
            _ship.OnModulaRemove += RemoveModuleHandler;
            Debug.Log("Solar Panel attached");
        }
    }

    private void MoveShip(Vector3 newPosition)
    {
        rb.AddForce(newPosition);
        _ship.SubstracEnergy(1);
        //Debug.Log("ENERGY: " + _ship.Energy);
    }

    private void RemoveModuleHandler(object sender, EventArgs args)
    {
        RemoveModule();
    }
    private void RemoveModule()
    {
        var firstModule = _moduleObjects.Keys.First();
        var gameObject = _moduleObjects[firstModule];
        gameObject.transform.parent = null;
        _moduleObjects.Remove(firstModule);
    }
}