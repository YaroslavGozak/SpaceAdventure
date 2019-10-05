using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeteliteController : MonoBehaviour
{
    private int rotationDegree = 2;
    private float verticalSpeed = 0.1F;
    // Update is called once per frame
    void Update()
    {
        var position = GetComponent<Renderer>().transform.position;
        var rotation = GetComponent<Renderer>().transform.rotation;
        var setelite = GameObject.Find("Setelite");
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Renderer>().transform.position = new Vector3(position.x, position.y + verticalSpeed, position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Renderer>().transform.position = new Vector3(position.x, position.y - verticalSpeed, position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            var angles = rotation.eulerAngles;
            angles.z += rotationDegree;
            setelite.transform.Rotate(angles, rotationDegree);
        }
        if (Input.GetKey(KeyCode.D))
        {
            var angles = rotation.eulerAngles;
            angles.z += rotationDegree;
            setelite.transform.Rotate(angles, -rotationDegree);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collapsed");
        if(collision.gameObject.tag == "trash")
        {
            Application.Quit();
        }
    }
}
