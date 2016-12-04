using UnityEngine;
using System.Collections;

public class MoonParallax : MonoBehaviour {

    public float scrollSpeed;
    private Renderer _renderer;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        float y = Mathf.Repeat(Time.time * scrollSpeed / 2, 1);

        _renderer.sharedMaterial.mainTextureOffset = new Vector2(x, y);
	
	}
}
