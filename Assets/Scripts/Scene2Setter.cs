using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2Setter : MonoBehaviour
{
    public MongoDBConnector mongoDBConnector;
    
    public Dropdown dropdown;
    public List<string> optionsList;
    private static string selectedOption = "";
    
    private void Start()
    {
        Text titolo_Vino = GameObject.Find("Canvas/Panel/Panel/txtTitoloVino").GetComponent<Text>();
        Text sottotitolo_Vino = GameObject.Find("Canvas/Panel/Panel/txtSottotitoloVino").GetComponent<Text>();

        mongoDBConnector.GetDataAsync((data) =>
        {
            if (data != null)
            {
                titolo_Vino.text = data.document.Titolo1;
                sottotitolo_Vino.text = data.document.Titolo2;
                dropdown.ClearOptions();
                List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
                
                foreach (var element in data.document.Descrizione)
                {
                    optionsList.Add(element.Key);
                }
    
                foreach (string optionText in optionsList)
                {
                    Dropdown.OptionData option = new Dropdown.OptionData(optionText);
                    dropdownOptions.Add(option);
                }
    
                dropdown.AddOptions(dropdownOptions);
                
                dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

                selectedOption = optionsList[0];
                
            }
            else
            {
                titolo_Vino.text = "Errore, server non disponibile!";
            }
        }, "tit1_tit2_descr");
        
        
    }
    
    public void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < optionsList.Count)
        {
            selectedOption = optionsList[index];
        }
    }

    public static string GetSelectedOption()
    {
        return selectedOption;
    }
   


}