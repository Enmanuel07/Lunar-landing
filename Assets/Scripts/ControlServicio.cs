using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class ControlServicio : MonoBehaviour {

    // Use this for initialization
    static public string direccion = "http://localhost:8080/ServicioAplicada/records";
    public Text textoRecords;
    private string record;
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        textoRecords.text = record;
    }
    public static void GuardarEnServicio()
    {

        WWWForm form = new WWWForm();
        form.AddField("nombreJugador", ConfiguracionGlobal.nombreJugador);
        form.AddField("puntos", ControlJuego.puntaje);
        form.AddField("nivel", 1);

        WWW www = new WWW(direccion, form);
    }
    public void ObtenerRecords()
    {
        HttpWebRequest req = WebRequest.Create(direccion) as HttpWebRequest;
        string result = null;
        using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
        {
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            result = reader.ReadToEnd();
        }
        record = result.Replace("[{\"class\":\"servicioaplicada.Record\",\"id\":", "");
        record = record.Replace("{\"class\":\"servicioaplicada.Record\",\"id\":", "").Replace("\"", "");
        string[] records = record.Split('}');
        record = "";
        foreach(string value in records)
        {
            if(value.StartsWith(","))
                record += value.Substring(2) + "\n\n";
            else
                record += value.Substring(1) + "\n\n";
        }
        record = record.Replace("]","");
        
    }
}