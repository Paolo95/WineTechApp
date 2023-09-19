using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.EventSystems;

public class ScriptManager : MonoBehaviour
{
    public GameObject SoundManager;
    private string currentTime = "";
    private string[] splitDuration;
    List<string> scriptRedListed = new List<string>();
    List<string> scriptBlackListed = new List<string>();
    Text txtRecensione;
    private Dictionary<int, string> scriptDict = new Dictionary<int, string>();
    private List<String> scriptList = new List<string>();

    [SerializeField] ScrollRect autoScrollRect;
    [SerializeField] private RectTransform contentRectTransform;

    public MongoDBConnector mongoDBConnector;
    
    // Start is called before the first frame update
    void Start()
    {
        txtRecensione = GameObject
            .Find(
                "Canvas/Panel/Panel/ScrollViewAscolta/Viewport/Content/ScrollViewRecensione/Viewport/Content/txtRecensione")
            .GetComponent<Text>();
        
        mongoDBConnector.GetDataAsync((data) =>
        {
            if (data != null)
            {
                // Iterate through the string array
                for (int i = 0; i < data.document.ScriptSommelier.Count; i++)
                {
                    // Check if the entry is not empty
                    if (!string.IsNullOrEmpty(data.document.ScriptSommelier[i]))
                    {
                        scriptDict[i] = data.document.ScriptSommelier[i]; // Add the entry to the dictionary
                    }
                }
                
            }
            else
            {
                Debug.LogError("Impossibile connettersi al DB!");
            }

            
        });
        
        foreach (KeyValuePair<int, string> scriptScorePair in scriptDict)
        {
            scriptList.Add(scriptScorePair.Value);
        }
        
        txtRecensione.text = (string.Join("\n", scriptList));

        if (autoScrollRect == null) autoScrollRect = GetComponent<ScrollRect>();

    }



// Update is called once per frame
    void Update()
    {
        currentTime = SoundManager.GetComponent<SoundManager>().getCurrentTime();

        int script_minutes = 0;
        int script_seconds = 0;

        splitDuration = currentTime.Split(':');

        if (splitDuration.Length > 1)
        {
            if (!(String.IsNullOrEmpty(splitDuration[0])) || !(String.IsNullOrEmpty(splitDuration[1])))
            {
                script_minutes = Int32.Parse(splitDuration[0]);
                script_seconds = Int32.Parse(splitDuration[1]);
            }
        }

        int totalSeconds = 0;

        totalSeconds = script_minutes * 60 + script_seconds;

        scriptRedListed = scriptDict
            .Where(item => item.Key <= totalSeconds)
            .Select(item => item.Value)
            .ToList();

        scriptBlackListed = scriptDict
            .Where(item => item.Key > totalSeconds)
            .Select(item => item.Value)
            .ToList();

        if (scriptRedListed.Count > 0)
        {
            scriptBlackListed.Insert(0, "");
        }

        txtRecensione.text = "<color=#8F1338>" + string.Join("\n", scriptRedListed) + "</color>" +
                             string.Join("\n", scriptBlackListed);

        // Calculate the total height of the content
        float totalContentHeight = contentRectTransform.rect.height;

        // Calculate the height of a single line of text in the ScrollView
        float lineHeight = txtRecensione.fontSize + txtRecensione.lineSpacing;

        float highlightedTextPosition = (scriptRedListed.Count * lineHeight);

        // Calculate the height of the ScrollView viewport
        float scrollViewViewportHeight = autoScrollRect.viewport.rect.height;

        // Calculate the maximum vertical scroll position (bottom-most position of the ScrollView)
        float maxVerticalScrollPosition = totalContentHeight - scrollViewViewportHeight;

        // Calculate the target vertical scroll position based on the highlighted text position
        float targetVerticalScrollPosition = Mathf.Clamp(highlightedTextPosition, 0f, maxVerticalScrollPosition);

        // Calculate the normalized vertical scroll position (between 0 and 1)
        float normalizedScrollPosition = targetVerticalScrollPosition / maxVerticalScrollPosition;

        if (normalizedScrollPosition < 0.058f)
        {
            autoScrollRect.verticalScrollbar.value = 1f;
            
        }else if(normalizedScrollPosition > 0.058f && normalizedScrollPosition < 0.32f)
        {
            autoScrollRect.verticalScrollbar.value = 1f - normalizedScrollPosition;
            
        }else if(normalizedScrollPosition > 0.32f && normalizedScrollPosition < 0.46f)
        {
            autoScrollRect.verticalScrollbar.value = 1f - normalizedScrollPosition - 0.1f;
        }
        else if (normalizedScrollPosition > 0.46f && normalizedScrollPosition < 0.53f)
        {
            autoScrollRect.verticalScrollbar.value = 1f - normalizedScrollPosition - 0.18f;
        }else
        {
            autoScrollRect.verticalScrollbar.value = 0f;
        }
        
    }

}