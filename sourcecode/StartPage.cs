using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitButton() { 
        Application.Quit();
        Debug.Log("Game close");
    }

    // Update is called once per frame
    public void StartButton() 
    {
        SceneManager.LoadScene("Lobby");
    }
}
