using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfiguracionGlobal : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]static public bool musica;
    [HideInInspector]static public bool efectos;
    public bool musicaToggle;
    public bool efectosToggle;
    public bool dropDificultad;
    [HideInInspector]
    public enum Edificultad
    {
        MuyFacil,
        Facil,
        Moderado,
        Dificil
    }
    static public Edificultad dificultad;
    void Start()
    {
        if (musicaToggle)
            GetComponent<Toggle>().isOn = musica;
        if (efectosToggle)
            GetComponent<Toggle>().isOn = efectos;
        if (dropDificultad)
            GetComponent<Dropdown>().value = (int)dificultad;
    }
    	
	// Update is called once per frame
	void Update() {
        if (musicaToggle)
            musica = GetComponent<Toggle>().isOn;
        if (efectosToggle)
            efectos = GetComponent<Toggle>().isOn;
        if (dropDificultad)
            dificultad = (Edificultad)GetComponent<Dropdown>().value;
    }
}
