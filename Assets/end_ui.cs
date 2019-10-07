using UnityEngine;
using UnityEngine.UI;
using Uitry;
using System;

public class end_ui : MonoBehaviour
{
    public Text score;
    private Ship _ship = Ship.Instance;


    void FixedUpdate()
    {
        var score_num = Convert.ToInt32(_ship.Score);
        score.text = string.Format("You have exhausted your energy\n Num of destroyed satellites : {0:0}", score_num);
    }
}
