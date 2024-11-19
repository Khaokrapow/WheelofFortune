using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeatherCreation : ScriptableObject 
{ 
    public List<Weather> wheathers;

    public int wheathersCount()
    {
        return wheathers.Count;
    }
    public Weather getWeather(int index)
    {
        return wheathers[index];
    }

}
