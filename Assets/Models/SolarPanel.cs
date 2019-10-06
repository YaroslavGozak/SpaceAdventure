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

    private void OnCollisionEnter(Collision collision)
    { 
        _module.Damage(30);
        Debug.Log($"Solar panel hit. Health {_module.Health}");
    }
}
