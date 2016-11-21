using UnityEngine;
using System.Collections;

public class ControlItems : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider otro)
    {
        if(otro.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
