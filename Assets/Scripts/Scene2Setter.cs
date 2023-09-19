using UnityEngine;
using UnityEngine.UI;

public class Scene2Setter : MonoBehaviour
{
    public MongoDBConnector mongoDBConnector;
    
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
            }
            else
            {
                titolo_Vino.text = "Errore, server non disponibile!";
            }
        });
        
        
    }
}