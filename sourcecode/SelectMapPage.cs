using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapPage : MonoBehaviour
{
    public string nameMap;
    public static string selectMap = "";

    public void OpenScene()
    {
        selectMap = nameMap;
        SceneManager.LoadScene("UniversalMap");
    }

   
}
