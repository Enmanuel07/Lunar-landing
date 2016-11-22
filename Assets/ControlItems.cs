using UnityEngine;
using System.Collections;

public class ControlItems : MonoBehaviour {

    // Use this for initialization
    public GameObject propulsorEspacial;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider otro)
    {
        print("hola");
        if(otro.tag == "Player" && gameObject.tag == "Combustible")
        {
            propulsorEspacial.GetComponent<ControlNave>().combustible += 50;
            Destroy(gameObject);
        }
    }
}
