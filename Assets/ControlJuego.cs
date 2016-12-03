using UnityEngine;
using Assets.Utilities;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class ControlJuego : MonoBehaviour
{

    private LevelGenerator levelGenerator;

    public Transform levelDataGroup;
    
    public Transform ubicacionNave;

    public GameObject[] tiles;

    private float velocidadCam = 1.2f;

    public Text textoPuntaje;

    private int puntaje;
    private int puntajeAnterior;

    // Use this for initialization 
    void Start() {

        levelGenerator = new LevelGenerator(new Vector2(0, 0), tiles, "Nivel1", levelDataGroup);

        levelGenerator.GenerateLevel();
        puntaje = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoCamara();
        textoPuntaje.text = "Puntaje: " + puntaje;
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if(Input.GetButtonDown("Load")) {

            Load();

        }

        if(Input.GetButtonDown("Save")) {
            Save();
        }
    }
    void MovimientoCamara()
    {
        if (ubicacionNave != null)
        {
            puntajeAnterior = puntaje;
            int nuevoPuntaje = Mathf.RoundToInt(Mathf.Abs(ubicacionNave.position.y)) * 2;
            Vector2 nuevaPosicion = Vector2.Lerp(transform.position, ubicacionNave.position, Time.deltaTime * velocidadCam);
            Vector3 posicionCamara = new Vector3(nuevaPosicion.x, nuevaPosicion.y, -10f);
            transform.position = new Vector3(posicionCamara.x, posicionCamara.y, -10f);
            if (ubicacionNave.position.y < 0 && puntajeAnterior < nuevoPuntaje)
                puntaje = nuevoPuntaje;
        }
    }

    public void Save() {

        Debug.Log(Application.persistentDataPath);

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.OpenOrCreate);

        PlayerData playerData = new PlayerData();

        GameObject nave = GameObject.FindGameObjectWithTag("Player");

        playerData.fuel = nave.GetComponent<ControlNave>().combustible;
        playerData.xPosition = nave.GetComponent<ControlNave>().transform.position.x;
        playerData.yPosition = nave.GetComponent<ControlNave>().transform.position.y;

        bf.Serialize(file, playerData);

        file.Close();

    }

    public void Load() {

        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat")) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();

            GameObject nave = GameObject.FindGameObjectWithTag("Player");
            nave.transform.position = new Vector3(playerData.xPosition, playerData.yPosition);
            nave.GetComponent<ControlNave>().combustible = playerData.fuel;
        }

    }
}
