using System;
using UnityEngine;
using TMPro;


public class Continue : MonoBehaviour
{

    public void ButtonContinue()
    {
        EventManager.OnTimerStart();
    }
}