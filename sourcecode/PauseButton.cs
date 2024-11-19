using System;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    public void PButton()
    {
        EventManager.OnTimerStop();
    }
}