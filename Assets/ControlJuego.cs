using UnityEngine;
using Assets.Utilities;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class ControlJuego : MonoBehaviour
{

    private GameObject _instantiatedLevelDataGroup;
    public float levelWidth;
    public float levelHeight;

    private Level _currentLevel;

    private LevelGenerator _levelGenerator;

    public GameObject ship;
    public GameObject levelDataGroup;
    
    public Transform ubicacionNave;

    public GameObject[] tiles;
    public GameObject[] cityBackgrounds;
    public GameObject[] volcanoBackgrounds;

    public Text textoPuntaje;
    [HideInInspector]
    static public int puntaje;
    private int puntajeAnterior;

    public Level GetCurrentLevel() {
        return _currentLevel;
    }

    // Use this for initialization 
    void Start() {

        _currentLevel = Level.None;

        StartCoroutine(GoToNextLevel());

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
            Vector2 nuevaPosicion = Vector2.Lerp(transform.position, ubicacionNave.position, Time.deltaTime * ConfiguracionGlobal.velocidadCamara);
            Vector3 posicionCamara = new Vector3(nuevaPosicion.x, nuevaPosicion.y, -10f);
            transform.position = new Vector3(posicionCamara.x, posicionCamara.y, -10f);
            if (ubicacionNave.position.y < 0 && puntajeAnterior < nuevoPuntaje)
                puntaje = nuevoPuntaje;
        }
    }

    public IEnumerator GoToNextLevel() {


        if (_currentLevel != Level.None) {
            yield return new WaitForSecondsRealtime(5);
        }

        if (_instantiatedLevelDataGroup != null) {
            Destroy(_instantiatedLevelDataGroup);
        }
        
        _instantiatedLevelDataGroup = Instantiate(levelDataGroup);

        switch (_currentLevel) {
            case Level.None:
                _currentLevel = Level.City;
                _levelGenerator = new LevelGenerator(new Vector2(0, 10), tiles, "CityLevel", _instantiatedLevelDataGroup, cityBackgrounds);
                _levelGenerator.GenerateLevel();
                break;
            case Level.City:
                _currentLevel = Level.Volcano;
                _levelGenerator = new LevelGenerator(new Vector2(0, 10), tiles, "VolcanoLevel", _instantiatedLevelDataGroup, volcanoBackgrounds);
                _levelGenerator.GenerateLevel();
                break;
            case Level.Volcano:
                _currentLevel = Level.City;
                _levelGenerator = new LevelGenerator(new Vector2(0, 10), tiles, "CityLevel", _instantiatedLevelDataGroup, cityBackgrounds);
                _levelGenerator.GenerateLevel();
                break;
            default:
                break;
        }

        ship.GetComponent<ControlNave>().Landed(false);
        ubicacionNave.position = new Vector3(20, 40, 0);
        //ubicacionNave.position = Vector3.Lerp(ubicacionNave.position, new Vector3(20, 40, 0), 5f);
    }

    public void Save() {

        Debug.Log(Application.persistentDataPath);

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(ConfiguracionGlobal.ruta + "/PlayerData.dat", FileMode.OpenOrCreate);

        PlayerData playerData = new PlayerData();

        GameObject nave = GameObject.FindGameObjectWithTag("Player");

        playerData.fuel = nave.GetComponent<ControlNave>().combustible;
        playerData.xPosition = nave.GetComponent<ControlNave>().transform.position.x;
        playerData.yPosition = nave.GetComponent<ControlNave>().transform.position.y;
        playerData.puntaje = puntaje;

        bf.Serialize(file, playerData);

        file.Close();

    }

    public void Load() {

        if (File.Exists(ConfiguracionGlobal.ruta + "/PlayerData.dat")) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(ConfiguracionGlobal.ruta + "/PlayerData.dat", FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            GameObject nave = GameObject.FindGameObjectWithTag("Player");
            nave.transform.position = new Vector3(playerData.xPosition, playerData.yPosition);
            nave.GetComponent<ControlNave>().combustible = playerData.fuel;
            puntaje = playerData.puntaje;
        }

    }
}
