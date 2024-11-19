using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class CharacterManager : MonoBehaviour
{
    public CharacterCreation characterDB;
    public TMP_Text nameText;
    public TMP_Text abilityText;
    public Sprite artworkSprite;
    public Image charImage; 
    private int selectOption = 0;
    public Image selected;
    public AudioSource clickButtonSound;
    private int rememberSelect = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption")) {
            selectOption = 0;
        }
        else{
            load();
        }
        rememberSelect = selectOption;
        updateCharacter(selectOption);
        selected.enabled = true;
    }

    public void nextOption() { 
        selectOption += 1;
        if (selectOption >=  characterDB.characterCount()) {
            selectOption = 0;
        }
        updateCharacter(selectOption);
        if (selectOption == rememberSelect)
        {
            selected.enabled = true;
        }
        else
        {
            selected.enabled = false;
        }
        clickButtonSound.Play();
    }

    public void backOption()
    {
        selectOption -= 1;
        if (selectOption < 0)
        {
            selectOption = characterDB.characterCount() -1;
        }
        updateCharacter(selectOption);
        if (selectOption == rememberSelect)
        {
            selected.enabled = true;
        }
        else
        {
            selected.enabled = false;
        }
        clickButtonSound.Play();

    }

    public void updateCharacter(int selectOption) {
        abilityText.text = "";
        Character character = characterDB.getCharacter(selectOption);
        artworkSprite = character.imageCharacter;
        charImage.sprite = artworkSprite;
        nameText.text = character.nameChar;
        checkAbility(character.abilityCode.ToString(), character.value);
         
    }

    private void load()
    {
        selectOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void save() { 
        PlayerPrefs.SetInt("selectedOption", selectOption);
    }
    public void selectedOption()
    {
        save();
        selected.enabled = true;
        rememberSelect = selectOption;
        clickButtonSound.Play();
    }

    public void changeScene() {
      
        SceneManager.LoadScene("Lobby");
        clickButtonSound.Play();
    }

    public void checkAbility(string textAbi, int value) {
        switch (textAbi)
        {
            case "UNSPECIFIED":
                //abilityText.text += "No Ability";
                abilityText.text += "ไม่มีทักษะความสามารถ";
                break;

            case "MAX_SPEED":
                //abilityText.text += " + "+ value.ToString() + " Max Speed";
                abilityText.text += "เพิ่มความเร็วสูงสุดของรถ " + value.ToString() + " กิโลเมตร/ชั่วโมง";
                break;

            case "WEATHER_RAIN":
                //abilityText.text += "Protect rain in map" ;
                abilityText.text += "เพิ่มความเร็วสูงสุดของรถ " + value.ToString() + " กิโลเมตร/ชั่วโมง เมื่อต้องเจอกับสถาพอากาศฝนพรำ";
                break;

            case "WEATHER_WIND":
                //abilityText.text += "Protect wind in map";
                abilityText.text += "เพิ่มความเร็วสูงสุดสำหรับการถอยหลังของรถ " + value.ToString() + " กิโลเมตร/ชั่วโมง เมื่อต้องเจอกับสถาพอากาศลมแรง";
                break;

            case "WEATHER_SMOKE":
                //abilityText.text += "Protect smoke in map ";
                int valueDivide = value / 2;
                abilityText.text += "จะถูกลดความเร็วสูงสุดของรถเพียง " + valueDivide.ToString() + " กิโลเมตร/ชั่วโมง เมื่อต้องเจอกับสถาพอากาศควัน";
                break;

            case "WEATHER_SNOW":
                //abilityText.text += "Protect snow in map ";
                int valueDivide2 = value / 2;
                abilityText.text += "จะถูกลดความเร็วสูงสุดสำหรับการถอยหลังของรถเพียง " + valueDivide2.ToString() + " กิโลเมตร/ชั่วโมง เมื่อต้องเจอกับสถาพอากาศหิมะ";
                break;

            case "ADD_TIME":
                //abilityText.text += "There's a " + value.ToString() + "% chance to reduce the time by 10 seconds when picking up a negative item.";
                abilityText.text += "มีโอกาส " + value.ToString() + "% ที่จะได้รับการบวกเวลาเพิ่มขึ้นจากเดิม 10 วินาที";
                break;

            case "SUB_TIME":
                //abilityText.text += "There's a " + value.ToString() + "% chance to gain 10 extra seconds when picking up a positive item.";
                abilityText.text += "มีโอกาส " + value.ToString() + "% ที่จะได้รับการลดเวลาที่ถูกลบลงจากเดิม 10 วินาที";
                break;

        }

    }
}
