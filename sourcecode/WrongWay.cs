using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WrongWay : MonoBehaviour
{
    private QuizUIManager quizUIManager; // Set in the Inspector
    private UISlideTrigger uiSlideTrigger; // Set in the Inspector
    public float delayBeforeClearingText = 3f; // Adjustable delay
    private AudioSource audio;
    private bool hasTriggered = false;

    void Start()
    {
        quizUIManager = FindObjectOfType<QuizUIManager>();
        uiSlideTrigger = FindObjectOfType<UISlideTrigger>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = quizUIManager.wrongWaySound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Set true Player Can Enter Once
            PlaySound();
            Debug.Log("Wrong");
            if (uiSlideTrigger != null)
            {
                uiSlideTrigger.HitAnswer();
            }

            StartCoroutine(HandleWrongWay());
        }
    }

    public IEnumerator HandleWrongWay()
    {
        quizUIManager.ChooseWrongAnswer();
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeClearingText);

        if (quizUIManager != null)
        {
            quizUIManager.HideUI();
            quizUIManager.ShowBigTime();
        }
        // Wait time until Sound Play Complete
        if (audio != null && audio.clip != null)
        {
            yield return new WaitForSeconds(audio.clip.length);
        }
        // Destroy the game object
        Destroy(gameObject);
    }
    public void PlaySound()
    {
        audio.Play();
    }
}
