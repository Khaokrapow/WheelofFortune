using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class ButtonInUniversal : MonoBehaviour
{
    // ����Ѻ ��ʹ load
    public GameObject LoaderUI;
    public Slider progressSlider;
    //[SerializeField] RectTransform fader;

    // �׹�ѹ��ѧ����ѧ�����

    /*public void Start()
    {
        fader.gameObject.SetActive(true);
         LeanTween.alpha (fader, 1, 0);
         LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
           fader.gameObject.SetActive (false);
         });
    }*/

    public void ConfirmSelectMap()
    {
        string map = SelectMapPage.selectMap;
        //Ẻ���
        //SceneManager.LoadScene(map);

        //Ẻ����
        LoadScene(map);
        /*LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(map);
        });*/
    }

    // ��Ѻ�˹�����͡���
    public void GoBackSelectMap()
    {
        // ALPHA
        /*LeanTween.alpha (fader, 0, 0);
        LeanTween.alpha (fader, 1, 0.5f).setOnComplete (() => {
            SceneManager.LoadScene("Select Map");
        });*/
        SceneManager.LoadScene("Select Map");
    }

    // ��Ѻ�˹����ͺ���
    public void GoBackLobby() {
        /*LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene("Lobby");
        });*/
        SceneManager.LoadScene("Lobby");
    }

    public void GoBackSelectCar()
    {
        /*LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene("Select Car");
        });*/
        SceneManager.LoadScene("Lobby");
    }

    public void LoadScene(string map)
    {
        StartCoroutine(LoadScene_Coroutine(map));
    }

    public IEnumerator LoadScene_Coroutine(string map)
    {
        progressSlider.value = 0;
        LoaderUI.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(map);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
