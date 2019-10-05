using System.Collections;
using System.Collections.Generic;
using Uitry;
using UnityEngine;

public class SolarPanel : MonoBehaviour
{
    private IModule _module;
    // Start is called before the first frame update
    void Start()
    {
        _module = new SolarPanelModule();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {

        }
    }
}
