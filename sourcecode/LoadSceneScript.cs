using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    

    [Tooltip("What is the name of the scene we want to load when clicking the button?")]
    public string SceneName;

    public void LoadTargetScene()
    {

        AudioListener.pause = false;
        Time.timeScale = 1;
        Debug.Log("Load new Scene");
        SceneManager.LoadScene(SceneName);

        AudioListener.pause = false;
        Time.timeScale = 1;


    }

}
