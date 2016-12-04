using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Assets.Utilities;

public class ConfiguracionGlobal : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public enum Edificultad
    {
        MuyFacil,
        Facil,
        Moderado,
        Dificil
    }
    [HideInInspector]static public bool musica;
    [HideInInspector]static public bool efectos;
    [HideInInspector]static public float velocidadCamara;
    static public Edificultad dificultad;
    public bool musicaToggle;
    public bool efectosToggle;
    public bool dropDificultad;
    public bool inputVelCamara;
    void Start()
    {
        Load();
        if (musicaToggle)
            GetComponent<Toggle>().isOn = musica;
        if (efectosToggle)
            GetComponent<Toggle>().isOn = efectos;
        if (dropDificultad)
            GetComponent<Dropdown>().value = (int)dificultad;
        if (inputVelCamara)
            GetComponent<InputField>().text = ""+velocidadCamara;
    }
    	
	// Update is called once per frame
	void Update() {
        if (musicaToggle)
            musica = GetComponent<Toggle>().isOn;
        if (efectosToggle)
            efectos = GetComponent<Toggle>().isOn;
        if (dropDificultad)
            dificultad = (Edificultad)GetComponent<Dropdown>().value;
        if (inputVelCamara)
            velocidadCamara = float.Parse(GetComponent<InputField>().text);
    }
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/Configuraciones.dat", FileMode.OpenOrCreate);

        Configuraciones configuraciones = new Configuraciones();

        configuraciones.dificultad = (int)dificultad;
        configuraciones.efectos = efectos;
        configuraciones.musica = musica;
        configuraciones.velCamara = velocidadCamara;

        bf.Serialize(file, configuraciones);

        file.Close();
    }
    private void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/Configuraciones.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Configuraciones.dat", FileMode.Open);
            Configuraciones configuraciones = (Configuraciones)bf.Deserialize(file);
            file.Close();

            dificultad = (Edificultad)configuraciones.dificultad;
            efectos = configuraciones.efectos;
            musica = configuraciones.musica;
            velocidadCamara = configuraciones.velCamara;
        }

    }
}
