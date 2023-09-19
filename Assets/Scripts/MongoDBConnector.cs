using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class Document
{
    public string _id;
    public string Nome;
    public string Titolo1;
    public string Titolo2;
    public float Latitudine;
    public float Longitudine;
    public string Descrizione;
    public string LuogoProd;
    public List<string> ScriptSommelier;
}

[Serializable]
public class JsonResponse
{
    public Document document;
}

public class MongoDBConnector : MonoBehaviour
{
    private const string apiKey = "lCcATJK57juG3SHbs16wDjaeniiK41kqdcQt51evKg4yGbGi0sVbegi47Lcw4MK1";
    private const string url = "https://us-east-1.aws.data.mongodb-api.com/app/data-wftpe/endpoint/data/v1/action/findOne";
    
    private Action<JsonResponse> onDataReceived;

    public void GetDataAsync(Action<JsonResponse> callback)
    {
        onDataReceived = callback;

        // Create a JSON object with your request data
        string jsonData = "{\"collection\":\"Cantina\",\"database\":\"WineTech\",\"dataSource\":\"ClusterWineTech\",\"projection\":{},\"filter\":{\"Nome\":\"" + ObjectTrackingHandler.GetTrackedObjectName() + "\"}}";
        
        // Create a UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Request-Headers", "*");
        request.SetRequestHeader("api-key", apiKey);

        // Attach the request data
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // Send the request asynchronously
        request.SendWebRequest().completed += (op) =>
        {
            // Handle the response when the request is complete
            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response into a DocumentData object
                JsonResponse data = JsonUtility.FromJson<JsonResponse>(request.downloadHandler.text);
                onDataReceived?.Invoke(data); // Invoke the callback with the data
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                onDataReceived?.Invoke(null); // Invoke the callback with null to indicate an error
            }
        };
    }
}
