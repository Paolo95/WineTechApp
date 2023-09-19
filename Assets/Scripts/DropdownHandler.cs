using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public Text TextBox;
    public Text annoSelezionato;
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("2018");
        items.Add("2019");
        items.Add("2020");
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown);});

    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        
    }

    string getAnnoSelezionato(Dropdown dropdown)
    {
        return dropdown.options[dropdown.value].text;
    }
  
}
