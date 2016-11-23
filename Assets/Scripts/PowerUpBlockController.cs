using UnityEngine;
using System.Collections;

public class PowerUpBlockController : MonoBehaviour {

    public GameObject FireShield;
    private GameObject nave;

	// Use this for initialization
	void Start () {

        nave = GameObject.FindGameObjectWithTag("Player");
        	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {

        Instantiate(FireShield);
    }
}
