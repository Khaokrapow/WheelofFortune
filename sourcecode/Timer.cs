using System;
using UnityEngine;
using TMPro;
using System.Runtime.ConstrainedExecution;

public class Timer : MonoBehaviour
{
    #region Variables
    private TMP_Text timerText;

    enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;
    [SerializeField] public double timeToDisplay ;

    private double timeLimit;
    private bool _isRunning = true;
  
    #endregion

    private void Awake() { 
        timerText = GetComponent<TMP_Text>();
        timeLimit = timeToDisplay + 1;

    }

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;
    private void EventManagerOnTimerUpdate(double value) => timeToDisplay += value;

    private void Update()
    {   
        if (!_isRunning) return;
        
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            timerText.color = Color.red;
            return;
        }
        if (timerType == TimerType.Countdown && timeToDisplay > timeLimit)
        {
            timeToDisplay = timeLimit;
            return;
        }
        if (timerType == TimerType.Stopwatch && timeToDisplay < 0.00f)
        {
            timeToDisplay = 0.0f;
            return;
        }
        
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    // äÍà·Áà¾ÔèÁàÇÅÒ
    public static void AddTime()
    {
         EventManager.OnTimerUpdate(10.0f);
        
    }
    // äÍà·ÁÅ´àÇÅÒ
    public static void SubtractTime()
    {
        EventManager.OnTimerUpdate(-10.0f);
    }


}
