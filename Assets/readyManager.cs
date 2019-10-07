using UnityEngine;
using UnityEngine.SceneManagement;

public class readyManager : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene("mainScene");
    }

    void ReadyScene()
    {
        SceneManager.LoadScene("mainScene");
    }
}
