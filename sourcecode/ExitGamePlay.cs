using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGamePlay : MonoBehaviour
{   
    [SerializeField] RectTransform fader;
    public void Start()
    {
        fader.gameObject.SetActive(true);
         LeanTween.alpha (fader, 1, 0);
         LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
           fader.gameObject.SetActive (false);
         });
    }

    public void ExitButton()
    {
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene("Lobby");
        });
    }
}