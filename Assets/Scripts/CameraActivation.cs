using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraActivation : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    public RawImage cameraDisplay;

    void Start()
    {
        // Initialize the camera texture
        webcamTexture = new WebCamTexture();
        cameraDisplay.texture = webcamTexture;
        cameraDisplay.material.mainTexture = webcamTexture;

        // Start the camera
        webcamTexture.Play();
    }

    void OnDestroy()
    {
        // Release the camera when the scene is destroyed
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
            Destroy(webcamTexture);
        }
    }
}