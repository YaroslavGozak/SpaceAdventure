using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReferences : MonoBehaviour
{

    public Transform[] positions;
    private int index = 0;

    public Vector3 GetNextPosition()
    {
        Debug.Log("positions" + positions);
        if (index >= positions.Length)
            index = 0;
        Vector3 result = positions[index].position;
        Debug.Log("result" + result);
        Debug.Log("index" + index);
        index = index + 1;
        
        return result;
    }
}
