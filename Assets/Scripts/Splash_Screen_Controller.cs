using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash_Screen_Controller : MonoBehaviour
{

    public static int SceneNumber;

    void Start()
    {
        if (SceneNumber == 0)
        {
            StartCoroutine(toSplashScreen());
        }

        if (SceneNumber == 1)
        {
            StartCoroutine(toMainMenu());
        }

        IEnumerator toSplashScreen()
        {
            yield return new WaitForSeconds(1);
            SceneNumber = 1;
            SceneManager.LoadScene(0);
        }

        IEnumerator toMainMenu()
        {
            yield return new WaitForSeconds(2);
            SceneNumber = 2;
            SceneManager.LoadScene(1);
        }
    }

   
}
