using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuChangeScene : MonoBehaviour
{
    public void btn_change_scene()
    {
        SceneManager.LoadScene(1);
    }
}