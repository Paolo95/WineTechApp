using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class IotAPICaller: MonoBehaviour
{
    public string status { get; set; }
    public List<string> head { get; set; }
    public List<List<string>> data { get; set; }
    public int rows { get; set; }

    

    [SerializeField] private Text txtTempFoglia;
    [SerializeField] private Text txtUmidFoglia;
    [SerializeField] private Text txtTemp;
    [SerializeField] private Text txtUmid;
    [SerializeField] private Text txtPioggia;
    [SerializeField] private Text txtUltimoAgg;
    void Start()
    {
        //api response:
        //"{
        //  \"status\":\"succ\",
        //  \"head\":
        //      [
        //          \"ts\",
        //          \"coll_time\",
        //          \"temperature\",
        //          \"humidity\",
        //          \"rainfall_today\",
        //          \"rainfall_instantaneous\",
        //          \"rainfall_yesterday\",
        //          \"rainfall_total\",
        //          \"soil_temperature\",
        //          \"soil_moisture\",
        //          \"leaf_humidity\",
        //          \"leaf_temperature\",
        //          \"ext_str\",
        //          \"ext_var1\",
        //          \"ext_var2\"
        //      ],
        //  \"data\":
        //      [
        //          [
        //              \"2023-09-16 15:22:04.627\",
        //              \"2023-09-16 15:22:04.625\",
        //              23.00000,
        //              89.90000,
        //              0.00000,
        //              0.00000,
        //              3.50000,
        //              3.50000,
        //              -2184.50000,
        //              -2184.50000,
        //              2.00000,
        //              25.30000,
        //              0.00000,
        //              0.00000,
        //              0.00000
        //          ]
        //      ],
        //  \"rows\":1
        // }"
        
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://13.51.174.103:6041/rest/sql");
        request.ContentType = "application/json";
        request.Method = "POST";
        request.Headers.Add("Authorization", "Basic " + "cm9vdDp0YW9zZGF0YQ==");
        request.ContentType = "application/json";        
        
        Stream dataStream = request.GetRequestStream();
        string sql = "select * from station1.sensor_data where ts >= NOW - 48h order by ts DESC limit 1";
        byte[] byteArray = Encoding.ASCII.GetBytes(sql);
        dataStream.Write(byteArray, 0, byteArray.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        
        IotAPICaller tempFogliaLoader = JsonConvert.DeserializeObject<IotAPICaller>(responseString);
        
        txtTempFoglia.text = (tempFogliaLoader.data[0][11].ToString().Substring(0, 5) + " °C");
        txtUmidFoglia.text = (tempFogliaLoader.data[0][10].ToString().Substring(0, 3) + " %");
        txtTemp.text = (tempFogliaLoader.data[0][2].ToString().Substring(0, 5) + " °C");
        txtUmid.text = (tempFogliaLoader.data[0][3].ToString().Substring(0, 5) + " %");
        txtPioggia.text = (tempFogliaLoader.data[0][4].ToString().Substring(0, 3) + "  mm");
        
        DateTime dataUltimoAgg = DateTime.ParseExact(tempFogliaLoader.data[0][1].ToString().Substring(0, 10), "yyyy-MM-dd" , CultureInfo.InvariantCulture);
        DateTime oraUltimoAgg = DateTime.ParseExact(tempFogliaLoader.data[0][1].ToString().Substring(11, 5), "HH:mm", CultureInfo.InvariantCulture);
        txtUltimoAgg.text = (dataUltimoAgg.ToString("dd-MM-yyyy") + " " + oraUltimoAgg.AddHours(2).ToString("HH:mm"));

        //await GetWeaherData();
    }
    
    
    
    public void UpdateData()
    {
        txtTempFoglia.text = "";
        txtUmidFoglia.text = "";
        txtTemp.text = "";
        txtUmid.text = "";
        txtPioggia.text = "";
        txtUltimoAgg.text = "";
        
        Start();
    }
   
}
