using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Init : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewOsserva;
    [SerializeField] private GameObject scrollViewAscolta;
    [SerializeField] private GameObject scrollViewRacconta;
    [SerializeField] private TextMeshProUGUI txtCantina;
    [SerializeField] private TextMeshProUGUI txtLuogoProduzione;
    [SerializeField] private TextMeshProUGUI txtDescrizione;
    [SerializeField] private Text txtTitoloVino;
    [SerializeField] private Text txtSottotitoloVino;
    
    public MongoDBConnector mongoDBConnector;
    
    // Start is called before the first frame update
    void Start()
    {
        if (MenuUserChoices.GetUserChoice().Contains("osserva"))
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
            scrollViewOsserva.SetActive(true);
        } else if (MenuUserChoices.GetUserChoice().Contains("ascolta"))
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
            scrollViewAscolta.SetActive(true);
        } else if (MenuUserChoices.GetUserChoice().Contains("racconta"))
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
            scrollViewRacconta.SetActive(true);
        }
        
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
                    txtDescrizione.text = data.document.Descrizione[Scene2Setter.GetSelectedOption()];
                }
                else
                {
                    txtTitoloVino.text = "Errore nel server!";
                }
            }, "tit1_tit2_luogo_descr");
        }
        
    }

}
