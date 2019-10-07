using UnityEngine;
using UnityEngine.UI;
using Uitry;
using System;

public class score_ui : MonoBehaviour
{
    public Text score;
    private Ship _ship = Ship.Instance;


    void FixedUpdate()
    {
        var score_num = Convert.ToInt32(_ship.Score);
        score.text = string.Format("Destroyed Satellites : {0:0}", score_num);
    }
}
