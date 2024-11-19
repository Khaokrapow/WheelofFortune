using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public Slider progressBar;
    public float currentProgress = 0f;

    public void UpdateProgress(float percent)
    {
        currentProgress = percent;
        progressBar.value = Mathf.Clamp(currentProgress / 100f, 0f, 1f); // ทำให้ค่าอยู่ระหว่าง 0-1
    }
}