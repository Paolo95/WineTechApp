﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnOsservaController : MonoBehaviour
{
    public MongoDBConnector mongoDBConnector;
    
    [SerializeField] private TextMeshProUGUI txtCantina;
    [SerializeField] private TextMeshProUGUI txtLuogoProduzione;
    [SerializeField] private TextMeshProUGUI txtDescrizione;
    [SerializeField] private Text txtTitoloVino;
    [SerializeField] private Text txtSottotitoloVino;
    public void OnClickOsserva()
    {
        Text font_Ascolta = GameObject.Find("Canvas/Panel/Panel/btnAscolta/txtAscolta").GetComponent<Text>();
        Text font_Osserva = GameObject.Find("Canvas/Panel/Panel/btnOsserva/txtOsserva").GetComponent<Text>();
        Text font_Racconta = GameObject.Find("Canvas/Panel/Panel/btnRacconta/txtRacconta").GetComponent<Text>();
        font_Ascolta.fontSize = 30;
        font_Osserva.fontSize = 38;
        font_Racconta.fontSize = 30;
        font_Ascolta.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
        font_Osserva.font = Resources.Load<Font>("Fonts/UtopiaStd-SemiboldSubh");
        font_Racconta.font = Resources.Load<Font>("Fonts/UtopiaStd-Subh");
        GetData();
    }
    
    private void GetData()
    {
        if (string.IsNullOrEmpty(txtTitoloVino.text) || string.IsNullOrEmpty(txtSottotitoloVino.text) ||
            string.IsNullOrEmpty(txtCantina.text) || string.IsNullOrEmpty(txtLuogoProduzione.text) ||
            string.IsNullOrEmpty(txtDescrizione.text))
        {
            mongoDBConnector.GetDataAsync((data) =>
            {
                if (data != null)
                {
                    txtTitoloVino.text = data.document.Titolo1;
                    txtSottotitoloVino.text = data.document.Titolo2;
                    txtCantina.text = data.document.Titolo1;
                    txtLuogoProduzione.text = data.document.LuogoProd;
                    txtDescrizione.text = data.document.Descrizione["2019"];
                }
                else
                {
                    txtTitoloVino.text = "Errore nel server!";
                }
            }, "tit1_tit2_luogo_descr");
        }
    }

}
