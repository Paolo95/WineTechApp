using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BtnAscoltaController : MonoBehaviour
{
    public void OnClickAscolta()
    {
        Text font_Ascolta = GameObject.Find("Canvas/Panel/Panel/btnAscolta/txtAscolta").GetComponent<Text>();
        Text font_Osserva = GameObject.Find("Canvas/Panel/Panel/btnOsserva/txtOsserva").GetComponent<Text>();
        Text font_Racconta = GameObject.Find("Canvas/Panel/Panel/btnRacconta/txtRacconta").GetComponent<Text>();
        font_Ascolta.fontSize = 38;
        font_Osserva.fontSize = 30;
        font_Racconta.fontSize = 30;
        font_Ascolta.font = Resources.Load<Font>("Fonts/UtopiaStd-SemiboldSubh");
        font_Osserva.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
        font_Racconta.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
    
    }
    
    
}
