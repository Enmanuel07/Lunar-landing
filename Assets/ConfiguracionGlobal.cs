using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Assets.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConfiguracionGlobal : MonoBehaviour
{

    // Use this for initialization
    [HideInInspector]
    public enum Edificultad
    {
        MuyFacil,
        Facil,
        Moderado,
        Dificil
    }
    [HideInInspector]
    static public bool musica;
    [HideInInspector]
    static public bool efectos;
    [HideInInspector]
    static public float velocidadCamara;
    [HideInInspector]
    static public Edificultad dificultad;
    [HideInInspector]
    static public string ruta;
    [HideInInspector]
    static public string nombreJugador;
    public bool musicaToggle;
    public bool efectosToggle;
    public bool dropDificultad;
    public bool inputVelCamara;
    public bool inputNombre;
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
            GetComponent<InputField>().text = "" + velocidadCamara;
        if (inputNombre)
            GetComponent<InputField>().text = nombreJugador;
    }

    // Update is called once per frame
    void Update()
    {
        if (musicaToggle)
            musica = GetComponent<Toggle>().isOn;
        if (efectosToggle)
            efectos = GetComponent<Toggle>().isOn;
        if (dropDificultad)
            dificultad = (Edificultad)GetComponent<Dropdown>().value;
        if (inputVelCamara)
            velocidadCamara = float.Parse(GetComponent<InputField>().text);
        if (inputNombre)
            nombreJugador = GetComponent<InputField>().text;
    }
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(ruta + "/Configuraciones.dat", FileMode.OpenOrCreate);
        Configuraciones configuraciones = new Configuraciones();

        configuraciones.dificultad = (int)dificultad;
        configuraciones.efectos = efectos;
        configuraciones.musica = musica;
        configuraciones.velCamara = velocidadCamara;
        configuraciones.ruta = ruta;
        configuraciones.nombreJugador = nombreJugador;

        bf.Serialize(file, configuraciones);
        File.WriteAllText(Application.persistentDataPath + "/ultimaRuta.txt", ruta);

        file.Close();
    }
    private void Load()
    {
        string ultimaRuta = File.ReadAllText(Application.persistentDataPath + "/ultimaRuta.txt");
        if (File.Exists(ultimaRuta + "/Configuraciones.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(ultimaRuta + "/Configuraciones.dat", FileMode.Open);
            Configuraciones configuraciones = (Configuraciones)bf.Deserialize(file);
            file.Close();

            dificultad = (Edificultad)configuraciones.dificultad;
            efectos = configuraciones.efectos;
            musica = configuraciones.musica;
            velocidadCamara = configuraciones.velCamara;
            ruta = configuraciones.ruta;
            nombreJugador = configuraciones.nombreJugador;
        }
        else
            ruta = Application.persistentDataPath;

    }

    public void Ruta()
    {
        #if UNITY_EDITOR
            var path = EditorUtility.OpenFolderPanel("Ruta para guardar configuraciones", "", "");
            ruta = path;
        #endif
    }
}
