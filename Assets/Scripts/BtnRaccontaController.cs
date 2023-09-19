using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnRaccontaController : MonoBehaviour
{
    public void OnClickRacconta()
    {
        Text font_Ascolta = GameObject.Find("Canvas/Panel/Panel/btnAscolta/txtAscolta").GetComponent<Text>();
        Text font_Osserva = GameObject.Find("Canvas/Panel/Panel/btnOsserva/txtOsserva").GetComponent<Text>();
        Text font_Racconta = GameObject.Find("Canvas/Panel/Panel/btnRacconta/txtRacconta").GetComponent<Text>();
        font_Ascolta.fontSize = 30;
        font_Osserva.fontSize = 30;
        font_Racconta.fontSize = 38;
        font_Ascolta.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
        font_Osserva.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
        font_Racconta.font = Resources.Load<Font>("Fonts/UtopiaStd-SemiboldSubh");
    }

}
