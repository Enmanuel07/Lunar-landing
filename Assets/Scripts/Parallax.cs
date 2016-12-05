using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
        
    public float scrollSpeed;
    private Rigidbody _shipRb;

	// Use this for initialization
	void Start () {

        _shipRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

	
	}
	
	// Update is called once per frame
	void Update () {


        if (_shipRb != null) {
            float yOffset = Mathf.Repeat(_shipRb.velocity.y * scrollSpeed, 1);
            float xOffset = Mathf.Repeat(_shipRb.velocity.x * scrollSpeed, 1);

            GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(xOffset, yOffset);
        }
    }
}
