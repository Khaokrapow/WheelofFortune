using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SettingMenuManager : MonoBehaviour
{
    public Button backButton;
    public TMP_Dropdown graphicsDropdown;
    public AudioMixer mainAudioMixer;
    public Slider masterVol, musicVol, sfxVol;
    private float masterPlayerVol, musicPlayerVol, sfxPlayerVol;
    private int graphicsQualityLevel;

    void Start()
    {
        LoadData();

        //set start sound
        mainAudioMixer.SetFloat("MasterVol", masterPlayerVol);
        mainAudioMixer.SetFloat("MusicVol", musicPlayerVol);
        mainAudioMixer.SetFloat("SFXVol", sfxPlayerVol);
        mainAudioMixer.SetFloat("GamePlayVol", 0.0f);
        mainAudioMixer.SetFloat("WinLoseVol", 0.0f);

        //set start slider
        masterVol.value = masterPlayerVol;
        musicVol.value = musicPlayerVol;
        sfxVol.value = sfxPlayerVol;
        // Set graphics quality dropdown
        graphicsDropdown.value = graphicsQualityLevel;
        graphicsDropdown.RefreshShownValue();
    }
    // sound value range = min(-80 // 0)max
    private void LoadData()
    {
        masterPlayerVol = PlayerPrefs.GetFloat("masterPlayerVol", -15.0f);
        musicPlayerVol = PlayerPrefs.GetFloat("musicPlayerVol", -15.0f);
        musicPlayerVol = PlayerPrefs.GetFloat("musicPlayerVol", -15.0f);
        sfxPlayerVol = PlayerPrefs.GetFloat("sfxPlayerVol", -15.0f);
        // 0-4 0verylow 1low 2med 3high 4very high
        graphicsQualityLevel = PlayerPrefs.GetInt("graphicsQualityLevel", 2);
        QualitySettings.SetQualityLevel(graphicsQualityLevel);
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("masterPlayerVol", masterPlayerVol);
        PlayerPrefs.SetFloat("musicPlayerVol", musicPlayerVol);
        PlayerPrefs.SetFloat("sfxPlayerVol", sfxPlayerVol);

        PlayerPrefs.SetInt("graphicsQualityLevel", graphicsDropdown.value);
    }


    public void BackToLobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void SetGraphicsQuality()
    {
        graphicsQualityLevel = graphicsDropdown.value;
        QualitySettings.SetQualityLevel(graphicsQualityLevel);
        SaveData();


    }
    public void ChangeMasterVolume(){
        mainAudioMixer.SetFloat("MasterVol",masterVol.value);
        masterPlayerVol = masterVol.value;
        SaveData();
        //Debug.Log(masterVol.value);
    }
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
        musicPlayerVol = musicVol.value;
        SaveData();
    }
    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
        sfxPlayerVol = sfxVol.value;
        SaveData();
    }

}
