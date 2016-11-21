using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlNave : MonoBehaviour {

    // Use this for initialization
    public ParticleEmitter propulsorCentro;
    public ParticleEmitter propulsorIzquierdo;
    public ParticleEmitter propulsorDerecho;
    public AudioSource sonidoPropulsor;
    public Text textoCombustible;
    private bool reproducirPropulsor;
    private float gravedad = 1.62519f;
    private float fuerzaPropulsor = 3f;
    private float combustible = 100;

	void Start () {
        reproducirPropulsor = false;
	}
	
	// Update is called once per frame
	void Update () {
        movimientoNave();
        if (reproducirPropulsor)
        {
            ReproducirPropulsor();
        }
        else
            sonidoPropulsor.Pause();
        textoCombustible.text = "Combustible:" + Mathf.Round(combustible) + "%";
    }
    void movimientoNave()
    {
        Quaternion rotationVector = GetComponent<Rigidbody>().rotation;
        if (combustible > 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<Rigidbody>().AddForce(transform.right * fuerzaPropulsor);
                rotationVector.z -= Input.GetAxis("Horizontal") * 0.1f * Time.deltaTime;
                propulsorIzquierdo.emit = true;
                propulsorDerecho.emit = false;
                combustible -= 5 * Time.deltaTime;
                reproducirPropulsor = true;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<Rigidbody>().AddForce(transform.right * -fuerzaPropulsor);
                rotationVector.z -= Input.GetAxis("Horizontal") * 0.1f * Time.deltaTime;
                propulsorIzquierdo.emit = false;
                propulsorDerecho.emit = true;
                combustible -= 5 * Time.deltaTime;
                reproducirPropulsor = true;
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                propulsorIzquierdo.emit = false;
                propulsorDerecho.emit = false;
                reproducirPropulsor = false;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * fuerzaPropulsor);
                propulsorCentro.emit = true;
                combustible -= 10 * Time.deltaTime;
                reproducirPropulsor = true;
            }
            if (Input.GetAxis("Vertical") == 0)
            {
                propulsorCentro.emit = false;
            }
            GetComponent<Rigidbody>().MoveRotation(rotationVector);
        }
        else
        {
            reproducirPropulsor = false;
            propulsorCentro.emit = false;
            propulsorDerecho.emit = false;
            propulsorIzquierdo.emit = false;
        }
        GetComponent<Rigidbody>().AddForce(transform.up * -gravedad);
    }
    private void ReproducirPropulsor()
    {
        if (sonidoPropulsor.isPlaying)
        {
            return;
        }
        sonidoPropulsor.Play();
    }
}
