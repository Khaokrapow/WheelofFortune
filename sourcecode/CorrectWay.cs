using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorrectWay : MonoBehaviour
{
    private QuizUIManager quizUIManager;
    private UISlideTrigger uiSlideTrigger;
    public float delayBeforeClearingText = 3f; // Adjustable delay
    private AudioSource audio;
    private bool hasTriggered = false;

    void Start()
    {
        quizUIManager = FindObjectOfType<QuizUIManager>();
        uiSlideTrigger = FindObjectOfType<UISlideTrigger>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = quizUIManager.correctWaySound;

    }
    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Set true Player Can Enter Once
            PlaySound();
            Debug.Log("Correct");
            if (uiSlideTrigger != null)
            {
                uiSlideTrigger.HitAnswer();
            }

            StartCoroutine(HandleCorrectWay());
        }
    }

    public IEnumerator HandleCorrectWay()
    {
        if (quizUIManager != null)
        {
            quizUIManager.ChooseCorrectAnswer();
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeClearingText);

        // Clear the UI text
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
