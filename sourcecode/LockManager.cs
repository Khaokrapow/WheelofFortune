using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LockManager : MonoBehaviour
{
    private int playerwinRound;

    [Header("Map1")]
    public Button Map1Button;

    [Header("Map2")]
    public Button Map2Button;
    public int roundNeedToUnLockMap2;
    private Image LockMap2;
    private Slider sliderMap2;

    [Header("Map3")]
    public Button Map3Button;
    public int roundNeedToUnLockMap3;
    private Image LockMap3;
    private Slider sliderMap3;


    [Header("Map4")]
    public Button Map4Button;
    public int roundNeedToUnLockMap4;
    private Image LockMap4;
    private Slider sliderMap4;

    void Start()
    {
        // หา GameObject ที่เป็นลูกของปุ่ม Map2 และ Map3 ซึ่งเป็นรูปล็อค
        LockMap2 = Map2Button.transform.Find("LockImage").GetComponent<Image>();
        sliderMap2 = Map2Button.transform.Find("Slider").GetComponent<Slider>();

        LockMap3 = Map3Button.transform.Find("LockImage").GetComponent<Image>();
        sliderMap3 = Map3Button.transform.Find("Slider").GetComponent<Slider>();


        LockMap4 = Map4Button.transform.Find("LockImage").GetComponent<Image>();
        sliderMap4 = Map4Button.transform.Find("Slider").GetComponent<Slider>();



        LoadWinRoundData();
        CheckWinRound();

        // set ui show
        if (roundNeedToUnLockMap2 > 0)
        {
            sliderMap2.value = (float)playerwinRound / (float)roundNeedToUnLockMap2;
        }
        else
        {
            sliderMap2.value = 1f; // full
        }

        if (roundNeedToUnLockMap3 > 0)
        {
            sliderMap3.value = (float)playerwinRound / (float)roundNeedToUnLockMap3;
        }
        else
        {
            sliderMap3.value = 1f; // full
        }


        if (roundNeedToUnLockMap4 > 0)
        {
            sliderMap4.value = (float)playerwinRound / (float)roundNeedToUnLockMap4;
        }
        else
        {
            sliderMap4.value = 1f; // full
        }


    }

    private void LoadWinRoundData()
    {
        playerwinRound = PlayerPrefs.GetInt("winRound", 0);
        Debug.Log("Player's win round : " + playerwinRound);
    }

    private void CheckWinRound()
    {
        // เช็ค Map 2
        if (playerwinRound >= roundNeedToUnLockMap2)
        {
           
            Map2Button.interactable = true;
            LockMap2.gameObject.SetActive(false); // ซ่อนภาพล็อค
            sliderMap2.gameObject.SetActive(false); // ซ่อนแทบ slider
        }
        else
        {
            Map2Button.interactable = false;
            LockMap2.gameObject.SetActive(true); // แสดงภาพล็อค
            sliderMap2.gameObject.SetActive(true); //  แสดงแทบ slider
        }

        // เช็ค Map 3
        if (playerwinRound >= roundNeedToUnLockMap3)
        {
            Map3Button.interactable = true;
            LockMap3.gameObject.SetActive(false); // ซ่อนภาพล็อค
            sliderMap3.gameObject.SetActive(false); // ซ่อนแทบ slider
        }
        else
        {
            Map3Button.interactable = false;
            LockMap3.gameObject.SetActive(true); // แสดงภาพล็อค
            sliderMap3.gameObject.SetActive(true); //  แสดงแทบ slider
        }
        // เช็ค Map 4
        if (playerwinRound >= roundNeedToUnLockMap4)
        {
            Map4Button.interactable = true;
            LockMap4.gameObject.SetActive(false); // ซ่อนภาพล็อค
            sliderMap4.gameObject.SetActive(false); // ซ่อนแทบ slider
        }
        else
        {
            Map4Button.interactable = false;
            LockMap4.gameObject.SetActive(true); // แสดงภาพล็อค
            sliderMap4.gameObject.SetActive(true); //  แสดงแทบ slider
        }
    }
}

