using UnityEngine;
using System.Collections;

public class ConfiguracionGlobal : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]static public bool musica;
    [HideInInspector]static public bool efecto;
	void Start () {
        musica = true;
        efecto = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setMusica(bool valor)
    {
        musica = valor;
    }
    public void setEfecto(bool valor)
    {
        efecto = valor;
    }
}
