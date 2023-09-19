using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene2 : MonoBehaviour
{
    public void btn_change_scene()
    {
        SceneManager.LoadScene(3);
    }
}
