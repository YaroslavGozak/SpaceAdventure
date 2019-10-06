using UnityEngine;
using Uitry;
using System;
using UnityEngine.UI;

public class energy_ui : MonoBehaviour
{
    private Ship _ship = Ship.Instance;
    public Text energy;


    // Update is called once per frame
    void Update()
    {
        var energy_num = Convert.ToInt32(_ship.Energy / 100);
        var energy_percent = _ship.Energy/10 / 10.0;
        string energy_string = "";


        energy_string += string.Format("Energy: {0:0.0}%\n", energy_percent);


        for (var i = 0; i < energy_num; i++)
        {
            energy_string += "|";
        }

        energy.text = energy_string;
    }
}
