using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Define a class structure to match the JSON data
[System.Serializable]
public class Coord
{
    public float lon;
    public float lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class Main
{
    public float temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public int pressure;
    public int humidity;
    public int sea_level;
    public int grnd_level;
}

[System.Serializable]
public class Wind
{
    public float speed;
    public int deg;
    public float gust;
}

[System.Serializable]
public class Clouds
{
    public int all;
}

[System.Serializable]
public class Sys
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}

[System.Serializable]
public class WeatherData
{
    public Coord coord;
    public Weather[] weather;
    public string baseData;
    public Main main;
    public int visibility;
    public Wind wind;
    public Clouds clouds;
    public int dt;
    public Sys sys;
    public int timezone;
    public int id;
    public string name;
    public int cod;
}

public class OpenWeatherAPICaller : MonoBehaviour
{
    private string openWeatherApiKey = "ea5f560544db2e6226f8c5da0c559f61";

    private float longitude = 0f;
    private float latitude = 0f;
    
    public MongoDBConnector mongoDBConnector;

    [SerializeField] private Image image;
    [SerializeField] private Text txtDescrizioneSalute;
    [SerializeField] private Text txtUltimoAgg;
    
    // Start is called before the first frame update
    async void Start()
    {
        if (longitude == 0f || latitude == 0f)
        {
            mongoDBConnector.GetDataAsync((data) =>
            {
                if (data != null)
                {
                    latitude = data.document.Latitudine;
                    longitude = data.document.Longitudine;
                }
            }, "lat_long");
        }
        
        await GetWeaherData();
    }
    
    private async Task GetWeaherData()
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={openWeatherApiKey}&units=metric";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var asyncOperation = webRequest.SendWebRequest();
            
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Errore nella richiesta: " + webRequest.error);
                txtUltimoAgg.text = "";
            }
            else
            {
                string json = webRequest.downloadHandler.text;
               
                WeatherData openWeatherData = JsonUtility.FromJson<WeatherData>(json);

                DateTime dateTime = UnixTimeStampToDateTime(openWeatherData.dt);
                
                string formattedDate = dateTime.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("it-IT"));

                txtUltimoAgg.text = formattedDate;
                
                // temporale
                
                if ((openWeatherData.weather[0].id > 201 && openWeatherData.weather[0].id < 233))
                {
                    image.sprite = Resources.Load<Sprite>("Images/attention");
                    txtDescrizioneSalute.text = "Oggi è una giornata difficile per il vigneto. C'e' un temporale in corso";
                    if (openWeatherData.wind.speed > 6f)
                    {
                        txtDescrizioneSalute.text = "Oggi è una giornata difficile per il vigneto. C'e' un temporale in corso con forte vento";
                    }
                }
                else if ((openWeatherData.weather[0].id > 310 && openWeatherData.weather[0].id < 322) ||
                            (openWeatherData.weather[0].id > 502 && openWeatherData.weather[0].id < 533))
                {
                    image.sprite = Resources.Load<Sprite>("Images/attention");
                    txtDescrizioneSalute.text = "Oggi nel vigneto sta piovendo molto... Potrebbe aumentare il rischio di malattie nel vigneto";
                    if (openWeatherData.wind.speed > 6f)
                    {
                        txtDescrizioneSalute.text = "Oggi è una giornata difficile per il vigneto...Sta piovendo molto e tira un forte vento. Potrebbe aumentare il rischio di malattie nel vigneto";
                    }
                }else if ((openWeatherData.weather[0].id >= 600 && openWeatherData.weather[0].id < 623))
                {
                    image.sprite = Resources.Load<Sprite>("Images/attention");
                    txtDescrizioneSalute.text = "Oggi nel vigneto sta nevicando! Potrebbe aumentare il rischio di malattie nel vigneto";
                    if (openWeatherData.wind.speed > 6f)
                    {
                        txtDescrizioneSalute.text = "Oggi nel vigneto sta nevicando e tira un forte vento! Potrebbe aumentare il rischio di malattie nel vigneto";
                    }
                }else if ((openWeatherData.weather[0].id > 800 && openWeatherData.weather[0].id < 805))
                {
                    image.sprite = Resources.Load<Sprite>("Images/ok");
                    txtDescrizioneSalute.text = "Oggi va tutto bene, ci sono soltanto delle nuvole...";
                    if (openWeatherData.wind.speed > 6f)
                    {
                        txtDescrizioneSalute.text = "Oggi nel vigneto tira un forte vento! Potrebbe danneggiare il vigneto...";
                    }
                }else if (openWeatherData.weather[0].id == 800)
                {
                    image.sprite = Resources.Load<Sprite>("Images/ok");
                    txtDescrizioneSalute.text = "Oggi va tutto bene nel vigneto!";
                }
                else
                {
                    image.sprite = Resources.Load<Sprite>("Images/ok");
                    txtDescrizioneSalute.text = "Oggi va tutto bene nel vigneto!";
                }
                
            /*
                 * {
                 *  "coord":
                 *      {
                 *         "lon":13.7589,
                 *         "lat":42.8142},
                 *         "weather":
                 *             [{
                 *                  "id":800,
                 *                  "main":"Clear",
                 *                  "description":"clear sky",
                 *                  "icon":"01d"}
                 *              ],
                 *          "base":"stations",
                 *          "main":
                 *              {
                 *                  "temp":299.41,
                 *                  "feels_like":299.41,
                 *                  "temp_min":295.96,
                 *                  "temp_max":301.19,
                 *                  "pressure":1016,
                 *                  "humidity":65,
                 *                  "sea_level":1016,
                 *                  "grnd_level":992
                 *              },
                 *          "visibility":10000,
                 *          "wind":
                 *              {
                 *                  "speed":3.41,
                 *                  "deg":62,
                 *                  "gust":2.6
                 *              },
                 *          "clouds":
                 *              {
                 *                  "all":0
                 *              },
                 *          "dt":1694272098,
                 *          "sys":
                 *              {
                 *                  "type":2,
                 *                  "id":2006527,
                 *                  "country":"IT",
                 *                  "sunrise":1694234268,
                 *                  "sunset":1694280450
                 *              },
                 *          "timezone":7200,
                 *          "id":3165549,
                 *          "name":"Torano Nuovo",
                 *          "cod":200
                 * }
            */
                
            }
        }
        
    }
    
    public async void UpdateOpenWeatherData()
    {
        image.sprite = Resources.Load<Sprite>("Images/loading");
        
        txtDescrizioneSalute.text = "";
        txtUltimoAgg.text = "";
        
        await GetWeaherData();
    }
    
    // Function to convert Unix timestamp to DateTime
    private DateTime UnixTimeStampToDateTime(long unixTimestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimestamp).ToLocalTime();
        return dateTime;
    }
}
