using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Camera cam;
    public float interactDist;

    public Transform holdPos;
    public float attractSpeed;

    public float minThrowForce;
    public float maxThrowForce;
    private float throwForce;

    private GameObject objectIHave;
    private Rigidbody objectRB;

    private Vector3 rotateVector = Vector3.one;

    private bool hasObject = false;



    private void Start()
    {
        throwForce = minThrowForce;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasObject)
        {
            DoRay();
        }

        if (Input.GetMouseButton(1) && hasObject)
        {
            throwForce += 0.1f;
        }

        if (Input.GetMouseButtonUp(1) && hasObject)
        {
            ShootObj();
        }

        if(Input.GetKeyDown(KeyCode.G) && hasObject)
        {
            DropObj();
        }

        if (hasObject)
        {
            RotateObj();

            if(CheckDist() >= 1f)
            {
                MoveObjToPos();
            }
        }



    }



    //----------------Polish Stuff
    private void CalculateRotVector()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        float z = Random.Range(-0.5f, 0.5f);

        rotateVector = new Vector3(x, y, z);
    }

    private void RotateObj()
    {
        objectIHave.transform.Rotate(rotateVector);
    }


    //----------------Functinoal Stuff

    public float CheckDist()
    {
        float dist = Vector3.Distance(objectIHave.transform.position, holdPos.transform.position);
        return dist;
    }

    private void MoveObjToPos()
    {
        objectIHave.transform.position = Vector3.Lerp(objectIHave.transform.position, holdPos.position, attractSpeed * Time.deltaTime);
    }

    private void DropObj()
    {
        objectRB.constraints = RigidbodyConstraints.None;
        objectIHave.transform.parent = null;
        objectIHave = null;
        hasObject = false;
    }

    private void ShootObj()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        DropObj();
    }

    private void DoRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDist))
        {
            if (hit.collider.CompareTag("trash"))
            {
                objectIHave = hit.collider.gameObject;
                objectIHave.transform.SetParent(holdPos);

                objectRB = objectIHave.GetComponent<Rigidbody>();
                objectRB.constraints = RigidbodyConstraints.FreezeAll;

                hasObject = true;

                CalculateRotVector();
            }
        }

    }

}
