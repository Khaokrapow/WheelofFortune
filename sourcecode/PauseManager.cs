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

    public Transform startPosition; // �ش������鹷���ͧ��á�˹�
    public Transform startPosition2;
    private Transform usePosition;
    public GameObject playerCar; // ö¹��ͧ������

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
        Time.timeScale = 0; // ��ش��

        // ����¹�� Pause Snapshot
        mainAudioMixer.FindSnapshot("PauseSnapshot").TransitionTo(0.01f);
        // �Դ���§��������Ŵ�дѺ���§
        mainAudioMixer.SetFloat("GamePlayVol", -80f); // �Դ���§ gameplay �� ö��о��
        mainAudioMixer.SetFloat("MusicVol", -80f); // �Դ���§�ŧ
    }

    public void ResumeGame()
    {
        inGameCanvas.SetActive(true);
        pauseGameCanvas.SetActive(false);
        Time.timeScale = 1; // ��������

        // ����¹�� Resume Snapshot
        mainAudioMixer.FindSnapshot("ResumeSnapshot").TransitionTo(0.01f);
        mainAudioMixer.SetFloat("GamePlayVol", 0f); // �Դ���§ gameplay ��Ѻ�׹
        mainAudioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("musicPlayerVol", 0.0f)); // �Դ���§�ŧ��Ѻ�׹
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
            // ��˹����˹��������Ѻö¹�������˹觢ͧ startPosition
            playerCar.transform.position = usePosition.position;

            // �ҡ��ͧ��� reset �����ع�ͧö¹�����
            playerCar.transform.rotation = usePosition.rotation;
            // �Դ confirm �Դ pause
            ResumeGame();
            respawnConfirmCanvas.SetActive(false);
        }
    }
}
