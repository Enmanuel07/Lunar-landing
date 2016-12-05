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
    public GameObject explosion;
    private ControlJuego controlJuego;
    private bool reproducirPropulsor;
    private float gravedad = 1.62519f;
    private float fuerzaPropulsor = 3f;
    public float combustible = 100;
    private bool _landed;
    private bool _isCloseToPlatform;
    private Transform _platform;

    void Start () {

        _landed = false;
        reproducirPropulsor = false;
        controlJuego = Camera.main.GetComponent<ControlJuego>();
	}

    public bool Landed() {
        return _landed;
    }

    public void Landed(bool landed) {
        _landed = landed;
    }
	
	// Update is called once per frame
	void Update () {

        if (!_landed) {
            movimientoNave();
        }
        

        if (_platform == null) {
            _platform = GameObject.FindGameObjectWithTag("Plataforma").transform;
        }

        if (reproducirPropulsor && ConfiguracionGlobal.efecto)
        {
            ReproducirPropulsor();
        }
        else
            sonidoPropulsor.Pause();
        if (Mathf.Round(combustible) > 100)
            combustible = 100;
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
                combustible -= 2 * Time.deltaTime;
                reproducirPropulsor = true;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<Rigidbody>().AddForce(transform.right * -fuerzaPropulsor);
                rotationVector.z -= Input.GetAxis("Horizontal") * 0.1f * Time.deltaTime;
                propulsorIzquierdo.emit = false;
                propulsorDerecho.emit = true;
                combustible -= 2 * Time.deltaTime;
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
                combustible -= 5 * Time.deltaTime;
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
        GetComponent<Rigidbody>().AddForce(transform.up * -gravedad, ForceMode.Acceleration);
    }

    void LateUpdate() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.5f, 39.5f), Mathf.Clamp(transform.position.y, -168f, 40f));
    }
    private void ReproducirPropulsor()
    {
        if (sonidoPropulsor.isPlaying)
        {
            return;
        }
        sonidoPropulsor.Play();
    }
    void OnCollisionEnter(Collision otro)
    {
        if (otro.gameObject.tag == "Plataforma")
        {
            if (otro.relativeVelocity.sqrMagnitude > 4)
            {
                DestruccionNave();

            } else {

                if (!_landed) {
                    _landed = true;
                    StartCoroutine(controlJuego.GoToNextLevel());
                    
                }
            }
        }
        else
        {
            DestruccionNave();
        }
    }

    public bool IsCloseToPlatform() {

        if (_platform == null) {
            return false;
        }
        return Vector3.Distance(_platform.position, transform.position) < 10;

    }

    //void OnCollisionExit(Collision other) {

    //    if (other.gameObject.CompareTag("Plataforma")) {

    //        _landed = false;
    //        Debug.Log("Saliendo de plataforma");
    //    }
        
    //}
    void DestruccionNave()
    {
        //controlJuego.Save(combustible);
        textoCombustible.text = "R para rintentar";
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
