using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowingCharacter : MonoBehaviour
{
    private readonly float _shift = 3;
    private readonly int _distanceAway = 10;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPosition = GameObject.Find("Setelite").transform.transform.position;
        GameObject.Find("Camera").transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - _distanceAway);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = GameObject.Find("Setelite").transform.transform.position;
        Vector3 cameraPosition = GameObject.Find("Camera").transform.position;

        if(playerPosition.y - cameraPosition.y > _shift)
        {
            GameObject.Find("Camera").transform.position = new Vector3(playerPosition.x, playerPosition.y - _shift, playerPosition.z - _distanceAway);
        }
        if (cameraPosition.y - playerPosition.y > _shift)
        {
            GameObject.Find("Camera").transform.position = new Vector3(playerPosition.x, playerPosition.y + _shift, playerPosition.z - _distanceAway);
        }
    }
}
