using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseGameCanvas;
    public GameObject inGameCanvas;

    public GameObject respawnConfirmCanvas;
    public GameObject exitConfirmCanvas;

    public Transform startPosition; // จุดเริ่มต้นที่ต้องการกำหนด
    public Transform startPosition2;
    private Transform usePosition;
    public GameObject playerCar; // รถยนต์ของผู้เล่น

    public AudioMixer mainAudioMixer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseCanvas()
    {
        pauseGameCanvas.SetActive(false);
        respawnConfirmCanvas.SetActive(false);
        exitConfirmCanvas.SetActive(false);
        usePosition = startPosition;
    }
    public void PauseGame()
    {
        inGameCanvas.SetActive(false);
        pauseGameCanvas.SetActive(true);
        Time.timeScale = 0; // หยุดเกม

        // เปลี่ยนเป็น Pause Snapshot
        mainAudioMixer.FindSnapshot("PauseSnapshot").TransitionTo(0.01f);
        // ปิดเสียงทั้งหมดโดยลดระดับเสียง
        mainAudioMixer.SetFloat("GamePlayVol", -80f); // ปิดเสียง gameplay เช่น รถและพลุ
        mainAudioMixer.SetFloat("MusicVol", -80f); // ปิดเสียงเพลง
    }

    public void ResumeGame()
    {
        inGameCanvas.SetActive(true);
        pauseGameCanvas.SetActive(false);
        Time.timeScale = 1; // เล่นเกมต่อ

        // เปลี่ยนเป็น Resume Snapshot
        mainAudioMixer.FindSnapshot("ResumeSnapshot").TransitionTo(0.01f);
        mainAudioMixer.SetFloat("GamePlayVol", 0f); // เปิดเสียง gameplay กลับคืน
        mainAudioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("musicPlayerVol", 0.0f)); // เปิดเสียงเพลงกลับคืน
    }
    public void RestartButton()
    {
        respawnConfirmCanvas.SetActive(true);
    }
    public void RefuseRestartButton()
    {
        respawnConfirmCanvas.SetActive(false);
    }
    public void ExitButton()
    {
        exitConfirmCanvas.SetActive(true);
    }
    public void RefuseExitButton()
    {
        exitConfirmCanvas.SetActive(false);
    }

    public void ChangeToPosition2()
    {
        usePosition = startPosition2;
    }
    public void RespawnButton()
    {
        if (playerCar != null && startPosition != null)
        {
            // กำหนดตำแหน่งใหม่ให้กับรถยนต์ตามตำแหน่งของ startPosition
            playerCar.transform.position = usePosition.position;

            // หากต้องการ reset การหมุนของรถยนต์ด้วย
            playerCar.transform.rotation = usePosition.rotation;
            // ปิด confirm ปิด pause
            ResumeGame();
            respawnConfirmCanvas.SetActive(false);
        }
    }
}
