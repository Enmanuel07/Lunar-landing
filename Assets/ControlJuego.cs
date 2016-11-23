using UnityEngine;
using System.Collections;
using Assets.Utilities;
using UnityEngine.SceneManagement;

public class ControlJuego : MonoBehaviour
{

    private LevelGenerator levelGenerator;

    public Transform levelDataGroup;
    
    public Transform ubicacionNave;

    public GameObject[] tiles;

    private float velocidadCam = 1.2f;

    // Use this for initialization 
    void Start() {

        levelGenerator = new LevelGenerator(new Vector2(0, 0), tiles, "Nivel1", levelDataGroup);

        levelGenerator.GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoCamara();
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }
    void MovimientoCamara()
    {
        if (ubicacionNave != null)
        {
            Vector2 nuevaPosicion = Vector2.Lerp(transform.position, ubicacionNave.position, Time.deltaTime * velocidadCam);
            Vector3 posicionCamara = new Vector3(nuevaPosicion.x, nuevaPosicion.y, -10f);
            transform.position = new Vector3(posicionCamara.x, posicionCamara.y, -10f);
        }
    }
}
