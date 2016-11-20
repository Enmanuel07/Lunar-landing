using UnityEngine;
using System.Collections;

public class ControlJuego : MonoBehaviour
{
    public Transform ubicacionNave;

    private float velocidadCam = 1.2f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoCamara();
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
