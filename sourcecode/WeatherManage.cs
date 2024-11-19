
using UnityEngine;
using DigitalRuby.RainMaker;
using Image = UnityEngine.UI.Image;
using UnityEngine.Audio;

public class WeatherManage : MonoBehaviour
{
    private Player player;
    public CharacterCreation characterDB;
    private GameObject objForCreateWeaTher; // Player
    private GameObject parent;
    public WeatherCreation weatherDB;
    private Weather weatherUse;
    public Image image;
    public Image iconWeather;
    private bool isHaveWeather = false;
    public AudioMixerGroup SFXOutput;


    void Start()
    {
        player = FindObjectOfType<Player>();
        //Debug.Log(player.getCharacterFromPlayer().nameChar);
        
        
        //Ai = GameObject.FindGameObjectWithTag("Bot");
        parent = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("1 : " + Ai.gameObject.GetComponent<AI>()..ToString() + " d ");
        //Debug.Log("1 : "+Ai.gameObject.GetComponent<AI>().character.nameChar + " ok ");
        if (SFXOutput == null)
        {
            Debug.LogError("Error: Please assign an output for the weatherManage in the Inspector!");
        }

        checkWeater();
        
        if (isHaveWeather) {
            createWeather();
        }

        player.applyAbilityWeather(weatherUse.weatherName, player.GetComponent<Player>().character);

    }

    public void createWeather()
    {
        switch (weatherUse.weatherName) {
            case "Rain":
                // rain
                image.sprite = weatherUse.imageWeather;
                objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y+10, parent.transform.position.z), weatherUse.weather.transform.rotation);
                objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                //objForCreateWeaTher.transform.localScale = new Vector3(5, 5, 5);

                // add on
                //GameObject objAddon = Instantiate(weatherUse.addOn, new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z+7), weatherUse.addOn.transform.rotation);
                //objAddon.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                //objAddon.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f) ;

                objForCreateWeaTher.gameObject.GetComponent<RainScript>().Camera = parent.GetComponentInChildren<Camera>();
                objForCreateWeaTher.gameObject.GetComponent<RainScript>().RainIntensity = 0.35f;
                objForCreateWeaTher.gameObject.GetComponent<RainScript>().EnableWind = false;
                //objForCreateWeaTher.gameObject.AddComponent<AudioSource>();
                //AudioSource audio = objForCreateWeaTher.gameObject.GetComponent<AudioSource>();
                //audio.clip = weatherUse.soundOfWheather;
                //audio.Play();

                iconWeather.sprite = weatherUse.iconWeather;
                break;

            case "Wind":
                image.enabled = false;
                objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x + 5, parent.transform.position.y +20, parent.transform.position.z + 5), weatherUse.weather.transform.rotation);
                objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                objForCreateWeaTher.transform.localScale = new Vector3(18, 18, 18);
                objForCreateWeaTher.transform.Rotate(0, 0, 180);
                objForCreateWeaTher.gameObject.AddComponent<AudioSource>();

                AudioSource audio1 = objForCreateWeaTher.gameObject.GetComponent<AudioSource>();
                audio1.clip = weatherUse.soundOfWheather;
                audio1.Play();

                audio1.outputAudioMixerGroup = SFXOutput;
                iconWeather.sprite = weatherUse.iconWeather;
                break;

            case "Smoke":
                image.sprite = weatherUse.imageWeather;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.07f);
                objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x, parent.transform.position.y +4, parent.transform.position.z), weatherUse.weather.transform.rotation);
                objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                objForCreateWeaTher.transform.localScale = new Vector3(18, 18, 18);
                iconWeather.sprite = weatherUse.iconWeather;
                break;

            case "Snow":
                image.sprite = weatherUse.imageWeather;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.05f);
                objForCreateWeaTher = Instantiate(weatherUse.weather, new Vector3(parent.transform.position.x-50, parent.transform.position.y+7, parent.transform.position.z), weatherUse.weather.transform.rotation);
                objForCreateWeaTher.transform.SetParent(gameObject.GetComponentInParent<Player>().transform);
                objForCreateWeaTher.GetComponentInChildren<ParticleSystem>().transform.localScale = new Vector3(5,5, 5);
                objForCreateWeaTher.gameObject.AddComponent<AudioSource>();

                AudioSource audio2 = objForCreateWeaTher.gameObject.GetComponent<AudioSource>();
                audio2.clip = weatherUse.soundOfWheather;
                audio2.Play();

                audio2.outputAudioMixerGroup = SFXOutput;
                iconWeather.sprite = weatherUse.iconWeather;
                break;

            case "Sunny":
                image.enabled = false;
                iconWeather.sprite = weatherUse.iconWeather;
                break;
        
        }

    }

    public void checkWeater()
    {
        int randomWheater = Random.Range(0,weatherDB.wheathersCount());
        image.enabled = true;
        isHaveWeather = true;
        weatherUse = weatherDB.getWeather(randomWheater);
        //for test
        //weatherUse = weatherDB.getWeather(0);


    }

    public string getWeatherName() {
        return weatherUse.weatherName;
    }

    public int checkAbilityWeatherForAI(EnumAbilityCode code) {
        int value = 0;
        switch (weatherUse.weatherName)
        {
            case "Rain":
                if(code==EnumAbilityCode.WEATHER_RAIN)
                {
                    value = 50;
                }

                break;

            case "Wind":
                if (code == EnumAbilityCode.WEATHER_WIND)
                {
                    value = 50;
                }
                break;

            case "Smoke":
                if (code == EnumAbilityCode.WEATHER_SMOKE)
                {
                    value = 25;
                }
                break;

            case "Snow":
                if (code == EnumAbilityCode.WEATHER_SNOW)
                {
                    value = 25;
                }
                break;

            case "Sunny":
                break;

        }
        return value;
    }
}
