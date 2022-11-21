using System;
using System.Collections;
using System.Data;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{

    /*
		In order to use this API, you need to register on the website.

		Source: https://openweathermap.org

		Request by city: api.openweathermap.org/data/2.5/weather?q={city id}&appid={your api key}
		Request by lat-long: api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={your api key}

		Api response docs: https://openweathermap.org/current
	*/

    public string apiKey = "56967efb1be3d4997ec4e64402cf0907";

    public string city;
    public bool useLatLng = false;
   
    public string morn;
    public float RotationSpeed;
    public Text temp;
    public Text main;
    public Text town;
    public Text peressure;
    public Text windSpeed;
    public Text humidity;
    public Text UV;
    public Text Visab;
    public Text vidch;
    public Text timeZon;
    public Text sunRise;
    public Text tempInd;
    public Text sunSet;

    public Image icon;
    public Sprite[] sprites;

    public InputField input; 
  

    public GameObject error;
    public GameObject vitryac;
    public GameObject Scrol;
   

    GPS GPScl = new GPS();

    private void Start()
    {
        Scrol.GetComponent<ScrollRect>().enabled = false;
    }
  
    public void GetRealWeather()
    {

        Scrol.GetComponent<ScrollRect>().enabled = true;

        string uri = "api.openweathermap.org/data/2.5/weather?";
       
            uri += "q=" + input.text + "&appid=" + apiKey;
        
        StartCoroutine(GetWeatherCoroutine(uri));
    }

    IEnumerator GetWeatherCoroutine(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log("Web request error: " + webRequest.error);
                error.SetActive(true);
            }
            else
            {
                ParseJson(webRequest.downloadHandler.text);
            }
        }
    }

    WeatherStatus ParseJson(string json)
    {
        WeatherStatus weather = new WeatherStatus();
        WeatherStatus curent = new WeatherStatus();
        try
        {
            dynamic obj = JObject.Parse(json);






            Debug.Log(weather.weatherId = obj.weather[0].id);
            Debug.Log(weather.main = obj.weather[0].main);
            Debug.Log(weather.description = obj.weather[0].description);
            Debug.Log(weather.temperature = obj.main.temp);
            Debug.Log(weather.pressure = obj.main.pressure);
            Debug.Log(weather.windSpeed = obj.wind.speed);
            weather.temperatureFeelsLike = obj.main.feels_like;
            weather.windDirectionValue = obj.wind.gust;
            weather.humidity = obj.main.humidity;
            weather.visibility = obj.visibility / 1000;

            sunRise.text = "Схід\n" + Convert.ToString(weather.UnixTimeToDateTime(Convert.ToDouble(obj.sys.sunrise)));

            sunSet.text = "Захід\n" + Convert.ToString(weather.UnixTimeToDateTime(Convert.ToDouble(obj.sys.sunset)));

            timeZon.text = "Час оновлення даних\n" + Convert.ToString(weather.UnixTimeToDateTime(Convert.ToDouble(obj.dt)));


            RotationSpeed = obj.wind.speed * 20;
            town.text = obj.name;
            main.text = obj.weather[0].main;
            temp.text = Math.Round(weather.Celsius()).ToString();
            peressure.text = "Атмосферний тиск\n" + obj.main.pressure + "mbar";
            windSpeed.text = "Швидкість вітру\n" + obj.wind.speed + "м/с";
            Visab.text = "Видимість\n" + weather.visibility + "км";
            humidity.text = "Вологість\n" + weather.humidity + "%";
            vidch.text = "Відчувається\n" + Math.Round(weather.Celsius2(), 1).ToString() + "*C";
            UV.text = "Пориви вітру\n" + weather.windDirectionValue + "м/с";
            tempInd.text = "*C";


            if (weather.weatherId == 800)
            {
                icon.sprite = sprites[0];
            }
            else if (weather.weatherId == 801)
            {
                icon.sprite = sprites[1];
            }
            else if (weather.weatherId == 802)
            {
                icon.sprite = sprites[2];
            }
            else if (weather.weatherId == 803)
            {
                icon.sprite = sprites[3];
            }
            else if (weather.weatherId == 804)
            {
                icon.sprite = sprites[3];
            }
            else if (weather.weatherId > 299 && weather.weatherId < 315 || weather.weatherId > 519 && weather.weatherId < 532)
            {
                icon.sprite = sprites[4];
            }
            else if (weather.weatherId > 499 && weather.weatherId < 505)
            {
                icon.sprite = sprites[5];
            }
            else if (weather.weatherId > 199 && weather.weatherId < 233)
            {
                icon.sprite = sprites[6];
            }
            else if (weather.weatherId > 599 && weather.weatherId < 623 || weather.weatherId == 511)
            {
                icon.sprite = sprites[7];
            }
            else if (weather.weatherId > 700 && weather.weatherId < 782)
            {
                icon.sprite = sprites[8];
            }



        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        
        return weather ;

        
      
    }



   


    


    TouchScreenKeyboard screenKeyboard;
    public Text text;
    string Pesudo;
    public void OpenKeyBoard()
    {
        screenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        if (TouchScreenKeyboard.visible == false && screenKeyboard != null)
        {
            if (screenKeyboard.done)
            {
                Pesudo = screenKeyboard.text;
                text.text = "///" + Pesudo;
                screenKeyboard = null;
               
            }
        } 
        float angle = transform.eulerAngles.z;
        vitryac.transform.Rotate(0, 0, RotationSpeed * 1f * Time.deltaTime);
    }
}


