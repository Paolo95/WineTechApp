using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public WebCamTexture wTexture = null;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindWithTag("Player");
        wTexture = new WebCamTexture();
        plane.GetComponent<Renderer>().material.mainTexture = wTexture;
        wTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
