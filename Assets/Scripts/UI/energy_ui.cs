using UnityEngine;
using Uitry;
using System;
using UnityEngine.UI;

public class energy_ui : MonoBehaviour
{
    private  int count = 0;
    private Ship _ship = Ship.Instance;
    public Text energy;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_ship.Energy > 0)
        {
            count = 0;
            var energy_num = Convert.ToInt32(_ship.Energy / 100);
            var energy_percent = _ship.Energy/10 / 10.0;
            string energy_string = "";

            energy.color = new Color32(0,255,247,255);

            energy_string += string.Format("Energy: {0:0.0}%\n", energy_percent);


            for (var i = 0; i < energy_num; i++)
            {
                energy_string += "|";
            }
        
            energy.text = energy_string;
        }
        else
        {
            if (count/20 % 2 == 0)
            {
                var reverce_count = 10 - count / 40;
                energy.color = Color.red;
                if(reverce_count >= 0)
                {
                    energy.text = "No ENERGY left! OFF in :" + reverce_count.ToString();
                }
                else
                {
                    energy.text = "Shutting Down";
                }
            }
            else
            {
                energy.text = "";
            }
            count++;
        }
         
    }
}
