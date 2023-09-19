using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUserChoices : MonoBehaviour
{
    private static String userChoice = "";
    
    // Start is called before the first frame update
    public void SetUserChoice(String choice)
    {
        userChoice = choice;
    }

    public static String GetUserChoice()
    {
        return userChoice;
    }
    
}
