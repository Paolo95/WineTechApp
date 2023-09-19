using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class txtCurrentTimeController : MonoBehaviour
{   

    public GameObject SoundManager;
    string currentTime = "";
    Text txtCurrTime;

    // Start is called before the first frame update
    void Start()
    {
        txtCurrTime = GameObject.Find("Canvas/Panel/Panel/ScrollViewAscolta/Viewport/Content/txtCurrTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = SoundManager.GetComponent<SoundManager>().getCurrentTime();
        txtCurrTime.text = currentTime;
    }
}
