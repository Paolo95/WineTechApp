using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] AudioClips;
    public int CurrentTrack = 0;
    public Slider ProgressBar;
    string currentTime = "";
    string totTime = "";
  

    void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioClips[CurrentTrack];
    }

    // Update is called once per frame
    void Update(){
        ProgressBar.maxValue = AudioClips[CurrentTrack].length;
        ProgressBar.value = audioSource.time;

        int currMinutes = Mathf.FloorToInt((audioSource.time % 3600)/60);
        int currSeconds = Mathf.FloorToInt(audioSource.time % 60); 

        currentTime = string.Format("{0}:{1:00}", currMinutes, currSeconds);
        
    }

    void Play(){
        audioSource.Play();
    }

    void Pause(){
        audioSource.Pause();
    }

    public string getCurrentTime(){
        return currentTime;
    }

    public string getTotalTime(){

        int totMinutes = Mathf.FloorToInt((AudioClips[CurrentTrack].length % 3600)/60);
        int totSeconds = Mathf.FloorToInt(AudioClips[CurrentTrack].length % 60);

        totTime = string.Format("{0}:{1:00}", totMinutes, totSeconds);
        
        return totTime;
    }


    public void onSliderClicked(){
        this.enabled = false;
    }

    public void onValueChanged(float value){
        if (value >= 0 && value < AudioClips[CurrentTrack].length) {
            audioSource.time = value;

            int currMinutes = Mathf.FloorToInt((audioSource.time % 3600) / 60);
            int currSeconds = Mathf.FloorToInt(audioSource.time % 60);

            currentTime = string.Format("{0}:{1:00}", currMinutes, currSeconds);
        }
        this.enabled = true;
    }

}
