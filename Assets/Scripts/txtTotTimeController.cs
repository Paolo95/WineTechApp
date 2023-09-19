using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class txtTotTimeController : MonoBehaviour
{
    public GameObject SoundManager;
    string totTime = "";
    Text txtTotTime;
    
    // Start is called before the first frame update
    void Start()
    {
        txtTotTime = GameObject.Find("Canvas/Panel/Panel/ScrollViewAscolta/Viewport/Content/txtTotTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        totTime = SoundManager.GetComponent<SoundManager>().getTotalTime();
        txtTotTime.text = totTime;
    }
}
