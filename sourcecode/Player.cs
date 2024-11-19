using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public CharacterCreation characterDB;
    public Sprite artworkSprite;
    public Image charImage;
    private int selectOption = 0;
    public Character character;
    public PrometeoCarController realCarController;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectOption = 0;
        }
        else
        {
            load();
        }
        updateCharacter(selectOption);
        //Debug.Log(weatherManage);
        
    }

    public void updateCharacter(int selectOption)
    {
        character = characterDB.getCharacter(selectOption);
        Debug.Log(character.nameChar);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        if(character.abilityCode == EnumAbilityCode.MAX_SPEED) {
            realCarController.maxReverseSpeed += character.value;
        }

    }

    private void load()
    {
        selectOption = PlayerPrefs.GetInt("selectedOption");
    }


    public bool hasWeatherAbility(Character character)
    {
        return character.abilityCode == EnumAbilityCode.WEATHER_RAIN || character.abilityCode == EnumAbilityCode.WEATHER_WIND || character.abilityCode == EnumAbilityCode.WEATHER_SMOKE || character.abilityCode == EnumAbilityCode.WEATHER_SNOW;
    }

    public bool hasTimeAbility() {
        return character.abilityCode == EnumAbilityCode.ADD_TIME || character.abilityCode == EnumAbilityCode.SUB_TIME; 
    }

    public void applyAbilityWeather(string weatherName , Character character)
    {   
        if (hasWeatherAbility(character))
        {
            switch (weatherName)
            {
                case "Rain":
                    //max speed
                    if (character.abilityCode == EnumAbilityCode.WEATHER_RAIN)
                    {
                        realCarController.maxSpeed += character.value *2;
                    }
                    else { 
                        realCarController.maxSpeed += character.value ;
                    }
                    break;

                case "Wind":
                    //max reverse speed
                    if (character.abilityCode == EnumAbilityCode.WEATHER_WIND)
                    {
                        //Debug.Log("before " + realCarController.maxSpeed);
                        realCarController.maxReverseSpeed += character.value * 2;
                        //Debug.Log("after " + realCarController.maxSpeed);
                    }
                    else
                    {
                        realCarController.maxReverseSpeed += character.value ;
                    }
                    break;

                case "Smoke":
                    if (character.abilityCode == EnumAbilityCode.WEATHER_SMOKE)
                    {
                        //Debug.Log("before " + realCarController.maxSpeed);
                        realCarController.maxSpeed -= character.value/2 ;
                        //Debug.Log("after " + realCarController.maxSpeed);
                    }
                    else {
                        realCarController.maxSpeed -= character.value ;
                        if (realCarController.maxSpeed <= 0)
                        {
                            realCarController.maxSpeed = 5;
                        }
                    }
                    
                    break;
                case "Snow":
                    if (character.abilityCode == EnumAbilityCode.WEATHER_SNOW)
                    {
                        //Debug.Log("before " + realCarController.maxSpeed);
                        realCarController.maxReverseSpeed -= character.value/2;
                        //Debug.Log("after " + realCarController.maxSpeed);
                    }
                    else {
                        realCarController.maxReverseSpeed -= character.value ;
                        if (realCarController.maxReverseSpeed <= 0) { 
                            realCarController.maxReverseSpeed = 5;
                        }
                    }
                   
                    break;
                case "Sunny":
                    break;
            }
        }
    }
    public bool percentAddOrSub() {
        int percent = 0;
        int randomPercent = Random.Range(0, 100);
        bool checkActiveSkill = false;
        if (hasTimeAbility()) {
            switch (character.abilityCode) {
                case EnumAbilityCode.ADD_TIME:
                    percent = character.value;
                    break;

                case EnumAbilityCode.SUB_TIME:
                    
                    percent = character.value;
                    break;
            }
            
        }
        if (percent >= randomPercent)
        {
            //Debug.Log("sub");
            checkActiveSkill = true;
        }

        return checkActiveSkill;
        //return true;
    }

    public EnumAbilityCode checkSkill() {
        return character.abilityCode;
    }

    public GameObject createVFX() {
        GameObject vfxSkill = Instantiate(character.VFX, new Vector3(260, 412, -507), character.VFX.transform.rotation);
        vfxSkill.SetActive(false);
        return vfxSkill;
    }
}