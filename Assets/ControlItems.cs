using UnityEngine;
using System.Collections;

public class ControlItems : MonoBehaviour {

    // Use this for initialization
    private GameObject propulsorEspacial;
	void Start () {

        propulsorEspacial = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider otro)
    {

        if(otro.tag == "Player" && gameObject.tag == "Combustible")
        {
            propulsorEspacial.GetComponent<ControlNave>().combustible += 50;
            Destroy(gameObject);
        }
    }
}
