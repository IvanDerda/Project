using System.Collections;
using System;
using UnityEngine;

public class WeatherStatus
{
    public int weatherId;
    public string main;
    public string description;
    public float temperature;
    public float temperatureForPeople;
    public float pressure;
    public float windSpeed;
    public float uv;
    public float temperatureFeelsLike;
    public float windDirectionValue;
    public float windDirection;
    public float humidity;
    public float visibility;
    public double sunrise;
    public double sunset;
    public double time;


    public string rise ;
    public string set;
    public string tim ;
    

    
    float year;
    int yearVisc;
    int day;
    int dayVisc;
    float croc5;
    public float Celsius()
    {
        return temperature - 273.15f;
    }
    public float Celsius2()
    {
        return temperatureFeelsLike - 273.15f;
    }
    public float Fahrenheit()
    {
        return Celsius() * 9.0f / 5.0f + 32.0f;
    }

   public DateTime UnixTimeToDateTime(double UnixTime)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0);
        return origin.AddSeconds(UnixTime);
    }

    public string Time (double time, string[] x)
    {
        x = time.ToString().Split(' ');
        return x[1];
    }

}