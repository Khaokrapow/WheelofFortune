using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;

public class Lap : MonoBehaviour
{
    public CarEngine round;

    private GameObject realPlayer;
    public GameObject EndGameCanvas;
    private GameObject WinTemplete;
    private GameObject LoseTemplete;


    public TMP_Text roundText;
    public TMP_Text speed;

    private String textCheck;
    //public TMP_Text checkText;
    Timer timer;
    float timeOFRealPlayer;

    public int speedOfCar;
    public bool isStop = false;
    private int countRound = 0;
    private int countAllCheckpointWithTrigger = 0;
    private int[] checkCountOfCheckpoint = new int[2];

    private int playRound;
    private int playerwinRound;
    private int wungwin;
    private int nakonwin;
    private int danwin;
    private int pawin;


    public AudioMixer mainAudioMixer;

    private float masterPlayerVol, musicPlayerVol, sfxPlayerVol;

    private PauseManager pauseManager;

    public AudioClip winSound; // คลิปเสียงที่จะเล่น
    public AudioClip loseSound; // คลิปเสียงที่จะเล่น
    public AudioSource audioSource; // ตัว AudioSource ที่จะใช้เล่นเสียง


    private void Start()
    {
        //Sound
        AudioListener.pause = false;
        masterPlayerVol = PlayerPrefs.GetFloat("masterPlayerVol", 0.0f);
        musicPlayerVol = PlayerPrefs.GetFloat("musicPlayerVol", 0.0f);
        sfxPlayerVol = PlayerPrefs.GetFloat("sfxPlayerVol", 0.0f);


        mainAudioMixer.SetFloat("MasterVol", masterPlayerVol);
        mainAudioMixer.SetFloat("MusicVol", musicPlayerVol);
        mainAudioMixer.SetFloat("SFXVol", sfxPlayerVol);
        mainAudioMixer.SetFloat("GamePlayVol", 0.0f);
        mainAudioMixer.SetFloat("WinLoseVol", 0.0f);
        // Time
        Time.timeScale = 1;
        textCheck = "Lose";
        //Debug.Log(round.roundToWin);
        //use round to win from AI enenmy
        roundText.text = countRound.ToString() + "/" + round.roundToWin + " รอบ";
        realPlayer = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(realPlayer.name);
        timer = FindObjectOfType<Timer>();

        for (int i = 0; i < 2 ;i++) {
            checkCountOfCheckpoint[i] = 0;

        }

        // หา PauseManager 
        pauseManager = FindObjectOfType<PauseManager>();

        // หา Win + Lose
        WinTemplete = EndGameCanvas.transform.Find("WinTemplete").gameObject;
        LoseTemplete = EndGameCanvas.transform.Find("LoseTemplete").gameObject;

        if (pauseManager == null)
        {
            Debug.LogError("PauseManager not found in the scene!");
        }
        pauseManager.CloseCanvas();

        // close panel
        EndGameCanvas.SetActive(false);
        WinTemplete.SetActive(false);
        LoseTemplete.SetActive(false);
        // Add and Load PlayRound
        playRound = PlayerPrefs.GetInt("playRound", 0);

        playRound += 1;
        PlayerPrefs.SetInt("playRound", playRound);
        PlayerPrefs.Save();
        // Load winRound

        playerwinRound = PlayerPrefs.GetInt("winRound", 0);
        wungwin = PlayerPrefs.GetInt("wungWinRound", 0);
        nakonwin = PlayerPrefs.GetInt("nakonWinRound", 0);
        danwin = PlayerPrefs.GetInt("danWinRound", 0);
        pawin = PlayerPrefs.GetInt("paWinRound", 0);

        Debug.Log("Player's win round: " + playerwinRound);
        Debug.Log("Player's wung win round: " + wungwin);
        Debug.Log("Player's nakon win round: " + nakonwin);
        Debug.Log("Player's dan win round: " + danwin);
        Debug.Log("Player's pa win round: " + pawin);


    }


    public void Update()
    {   
        
        speedOfCar = (int) realPlayer.GetComponent<PrometeoCarController>().carSpeed;
        speed.text = speedOfCar.ToString() + " กม./ชม. ";
        if (realPlayer.GetComponent<PrometeoCarController>().carSpeed <= 0)
        {
            speed.text = "0 กม./ชม.";
        }
        if (textCheck == "Lose") {
            timeOFRealPlayer += Time.deltaTime; 
        }
        //Debug.Log(round.getTimeAIWhenWin());

        //if (round.getTimeAIWhenWin() > 0) {
        //Debug.Log("player " + (int)timer.timeToDisplay);
        //}

        //Detected กรณีที่ผู้เล่นยังไม่เข้าเส้นชัยสักที
        // timer.timetodisplay ต้องเปิดหน้า canvas
        /*if ((round.getTimeAIWhenWin() > 0) && (round.getTimeAIWhenWin() + 60 <= (int)timer.timeToDisplay)) {
            textCheck = "End";
            LoseTemplete.SetActive(true);
            Time.timeScale = 0;
            //SceneManager.LoadScene("Lose");
        }*/
        if ((round.getTimeAIWhenWin() > 0) || (int)timer.timeToDisplay <= 0)
        {
            textCheck = "End";
            

            EndGameCanvas.SetActive(true);
            LoseTemplete.SetActive(true); //Lose
            Time.timeScale = 0;
            Debug.Log("Lose");
            // ปิดเสียงกลุ่ม gameplay ใน sfx
            mainAudioMixer.SetFloat("MusicVol", -80f);
            mainAudioMixer.SetFloat("GamePlayVol", -80f); // ปิดเสียง gameplay (เช่น รถ, พลุ)

            audioSource.clip = loseSound;
            // เล่นเสียง
            audioSource.Play();



        }
     }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CheckReturn"))
        {
            countAllCheckpointWithTrigger++;
            //Debug.Log("All" + countAllCheckpointWithTrigger);
            checkCountOfCheckpoint[collider.GetComponent<Checkpoint>().checkpointID]++;
            //Debug.Log("CheckPoint [ " + collider.GetComponent<Checkpoint>().checkpointID + " ] = " + checkCountOfCheckpoint[collider.GetComponent<Checkpoint>().checkpointID]);
            
            //case วนเข้าออกแลปแรก
            if (checkCountOfCheckpoint[0] >= 2 && checkCountOfCheckpoint[1] == 0) {
                checkCountOfCheckpoint[0] = 1;
                Debug.Log("เข้าช่วง 1 ");
            }
            //case วนเข้าออกแลปสอง
            if (checkCountOfCheckpoint[0] >= 1 && checkCountOfCheckpoint[1] >=1)
            {
                checkCountOfCheckpoint[1] = 1;
                Debug.Log("เข้าช่วง 2 ");
                pauseManager.ChangeToPosition2();
            }
            //case วนตรงตามเงื่อนไข
            if (checkCountOfCheckpoint[0] == 2 && checkCountOfCheckpoint[1] == 1) {
                countRound++;
                checkCountOfCheckpoint[0] = 1;
                checkCountOfCheckpoint[1] = 0;
                Debug.Log("เข้าช่วง 3 = ครบรอบ ");
            }
            roundText.text = countRound.ToString() + "/" + round.roundToWin + " รอบ";

        }
        
        //checkText.text = timer.timeToDisplay.ToString();
        if (collider.CompareTag("Stop")) {
            if (countRound >= round.roundToWin)
            {
                textCheck = "End";
                EndGameCanvas.SetActive(true);
                if ((textCheck == "End" && (round.getTimeAIWhenWin() == 0 ) && ((int)timer.timeToDisplay > 0)))
                {
                    //Debug.Log("AI = "+ round.getTimeAI());
                    //Debug.Log("Player = " + (int)timer.timeToDisplay);
                    WinTemplete.SetActive(true);
                    Time.timeScale = 0;
                    Debug.Log("Win");

                    AddWinRound();

                    // ปิดเสียงกลุ่ม gameplay ใน sfx
                    mainAudioMixer.SetFloat("MusicVol", -80f);
                    mainAudioMixer.SetFloat("GamePlayVol", -80f); // ปิดเสียง gameplay (เช่น รถ, พลุ)

                    audioSource.clip = winSound;
                    // เล่นเสียง
                    audioSource.Play();


                }
                else if (textCheck == "End" && (round.getTimeAIWhenWin() == 0) && (int)timer.timeToDisplay <= 0) 
                {
                    //Debug.Log("AI = " + round.getTimeAI());
                    //Debug.Log("Player = " + (int)timer.timeToDisplay);
                    LoseTemplete.SetActive(true);
                    Time.timeScale = 0;
                    Debug.Log("Lose");

                    // ปิดเสียงกลุ่ม gameplay ใน sfx
                    mainAudioMixer.SetFloat("MusicVol", -80f);
                    mainAudioMixer.SetFloat("GamePlayVol", -80f); // ปิดเสียง gameplay (เช่น รถ, พลุ)

                    audioSource.clip = loseSound;
                    // เล่นเสียง
                    audioSource.Play();

                }

                //timer.timeToDisplay //return time value when real player win!!
            }
        }
        
    }
    public int getCountCheckpoint() { 
        return countAllCheckpointWithTrigger;
    }

    public void AddWinRound() 
    {
        string map = SelectMapPage.selectMap;

        //winRound
        playerwinRound += 1;
        PlayerPrefs.SetInt("winRound", playerwinRound);
        
        if (map.Equals("วังบาดาลใต้แม่น้ำคำ"))
        {
            wungwin += 1;
            PlayerPrefs.SetInt("wungWinRound", wungwin);
            
        }
        else if (map.Equals("นครมอดไหม้"))
        {
            nakonwin += 1;
            PlayerPrefs.SetInt("nakonWinRound", nakonwin);
            
        }
        else if (map.Equals("แดนพิพากษา"))
        {
            danwin += 1;
            PlayerPrefs.SetInt("danWinRound", danwin);
            
        }
        else if (map.Equals("ป่าวงกต"))
        {
            pawin += 1;
            PlayerPrefs.SetInt("paWinRound", pawin);
            
        }

        PlayerPrefs.Save();

    }
    

}
