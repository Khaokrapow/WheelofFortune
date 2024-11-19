using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyPage : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    // ยืนยันไปยังแมปดังกล่าว

    public void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, 0.5f).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }
    public void PlayButton() {
        string map = SelectMapPage.selectMap;
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene("Select Map");
        });
    }

}
