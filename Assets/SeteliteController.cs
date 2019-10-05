using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeteliteController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var position = GetComponent<Renderer>().transform.position;
        var rotation = GetComponent<Renderer>().transform.rotation;
        var setelite = GameObject.Find("Setelite");
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Renderer>().transform.position = new Vector3(position.x, position.y + 0.1F, position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Renderer>().transform.position = new Vector3(position.x, position.y - 0.1F, position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            var angles = rotation.eulerAngles;
            angles.z += 2;
            Debug.Log("Rotating setelite + 2");
            setelite.transform.Rotate(angles, 2);
            Debug.Log(string.Format("Rotation: x {0}, y {1}, z {2}", setelite.transform.rotation.x, setelite.transform.rotation.y, setelite.transform.rotation.z));// + = new Quaternion(rotation.x, rotation.y, rotation.z + 1, rotation.w);
        }
        if (Input.GetKey(KeyCode.D))
        {
            var angles = rotation.eulerAngles;
            angles.z += 2;
            Debug.Log("Rotating setelite - 2");
            setelite.transform.Rotate(angles, -2);
            Debug.Log(string.Format("Rotation: x {0}, y {1}, z {2}", setelite.transform.rotation.x, setelite.transform.rotation.y, setelite.transform.rotation.z));//GetComponent<Renderer>().transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z - 1, rotation.w);
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
