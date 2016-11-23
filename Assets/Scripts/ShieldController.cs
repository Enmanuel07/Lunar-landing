using UnityEngine;
using System.Collections;
using Assets.Utilities;

public class ShieldController : MonoBehaviour {

    UniformCircularMotionBehaviour uniformCircularMotionBehaviour;
    private float _speed;
    private float _acceleration;
    private float _radius;

    // Use this for initialization
    void Start () {

        _speed = 20.0f;
        _acceleration = 1.0f;
        _radius = 2f;

        uniformCircularMotionBehaviour = new UniformCircularMotionBehaviour(_speed, _acceleration, _radius);

        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {

        Move();
    }

    void Move() {

        transform.localPosition = uniformCircularMotionBehaviour.GeneratePosition();

    }
}
