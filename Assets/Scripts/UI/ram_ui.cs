using UnityEngine;
using Uitry;
using System;
using UnityEngine.UI;

public class ram_ui : MonoBehaviour
{
    private Ship _ship = Ship.Instance;
    public Text ram;


    // Update is called once per frame
    void Update()
    {
        var ram_num = Convert.ToInt32(_ship.RAM);
        string ram_string = "";


        ram_string += string.Format("Free RAM: {0:0}\n", ram_num);


        for (var i = 0; i < ram_num; i++)
        {
            ram_string += " [|]";
        }

        ram.text = ram_string;
    }
}
