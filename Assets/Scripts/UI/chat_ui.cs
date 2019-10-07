using UnityEngine;
using UnityEngine.UI;
using Uitry;

public class chat_ui : MonoBehaviour
{
    public Text chat;
    private Ship _ship = Ship.Instance;


    void FixedUpdate()
    {
        chat.text = _ship.Mesage;
    }
}
