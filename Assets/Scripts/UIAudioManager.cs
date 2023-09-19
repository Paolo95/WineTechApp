using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PauseButton;
    public GameObject SoundManager;
    private int minutes;
    private int seconds;
 
    // Start is called before the first frame update
    void Start()
    {
        PauseButton.SetActive(false);
        PlayButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
            PlayButton.SetActive(false);
            PauseButton.SetActive(true);
            SoundManager.SendMessage("Play");
    }

    public void Pause(){
            PlayButton.SetActive(true);
            PauseButton.SetActive(false);
            SoundManager.SendMessage("Pause");
    }
}
