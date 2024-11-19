using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizUIManager : MonoBehaviour
{

    public TMP_Text quizText;
    public TMP_Text choice1Text;
    public TMP_Text choice2Text;

    public TMP_Text timer;
    public GameObject timeFrame;

    public GameObject choiceFrame;

    public TMP_Text timeSUBADDAnswer;

    public AudioClip correctWaySound;
    public AudioClip wrongWaySound;


    void Start()
    {
        HideUI();
        ShowBigTime();
        timeSUBADDAnswer.enabled = false;
    }
    public void HideUI()
    {
        quizText.gameObject.SetActive(false); 
        choice1Text.gameObject.SetActive(false); 
        choice2Text.gameObject.SetActive(false);
        choiceFrame.SetActive(false);

    }
    public void ShowUI()
    {
        quizText.gameObject.SetActive(true);
        choice1Text.gameObject.SetActive(true);
        choice2Text.gameObject.SetActive(true);
        choiceFrame.SetActive(true);

    }
    public void ShowSmallTime()
    {
        // กำหนดตำแหน่งและขนาดของ timer
        RectTransform timerRect = timer.gameObject.GetComponent<RectTransform>();
        timerRect.anchoredPosition = new Vector2(-40, 300);
        timer.fontSize = 40;

        // กำหนดตำแหน่งและขนาดของ timeFrame
        RectTransform timeFrameRect = timeFrame.GetComponent<RectTransform>();
        timeFrameRect.anchoredPosition = new Vector2(-40, 338);
        timeFrameRect.localScale = new Vector3(5.631816f, 1.681031f, 1);
    }

    public void ShowBigTime()
    {
        // กำหนดตำแหน่งและขนาดของ timer
        RectTransform timerRect = timer.gameObject.GetComponent<RectTransform>();
        timerRect.anchoredPosition = new Vector2(-32, 400);
        timer.fontSize = 70;

        // กำหนดตำแหน่งและขนาดของ timeFrame
        RectTransform timeFrameRect = timeFrame.GetComponent<RectTransform>();
        timeFrameRect.anchoredPosition = new Vector2(-41, 406);
        timeFrameRect.localScale = new Vector3(9.385578f, 3.1275f, 1);
    }

    IEnumerator hideTextAfterAnswer(int cooldown)
    {

        yield return new WaitForSeconds(cooldown);
        timeSUBADDAnswer.enabled = false;
    }

    public void ChooseCorrectAnswer() 
    {
        int showTime = 10;
        Timer.AddTime();
        timeSUBADDAnswer.enabled = true;
        timeSUBADDAnswer.text = "+ " + (showTime).ToString() + "s";
        timeSUBADDAnswer.color = Color.green;
        StartCoroutine(hideTextAfterAnswer(3));

    }

    public void ChooseWrongAnswer()
    {
        int showTime = 10;
        Timer.SubtractTime();
        timeSUBADDAnswer.enabled = true;
        timeSUBADDAnswer.text = "- " + (showTime).ToString() + "s";
        timeSUBADDAnswer.color = Color.red;
        StartCoroutine(hideTextAfterAnswer(3));

    }
}
