using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Rigidbody Trash;
    public int rotationMultipier;

    Vector3 angularVelocity;
    
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
