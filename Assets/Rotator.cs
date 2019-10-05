using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Rigidbody Trash;
    Vector3 angularVelocity;
    public int rotationMultipier;

    private void Start()
    {
        angularVelocity = rotationMultipier * Random.insideUnitSphere;
        Trash = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(angularVelocity * Time.deltaTime);
        Trash.MoveRotation(Trash.rotation * deltaRotation);
        
    }
}
