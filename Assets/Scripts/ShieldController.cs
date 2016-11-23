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

        _speed = 5.0f;
        _acceleration = 1.0f;
        _radius = 1.0f;

        uniformCircularMotionBehaviour = new UniformCircularMotionBehaviour(_speed, _acceleration, _radius);

	}
	
	// Update is called once per frame
	void Update () {

        Move();
    }

    void Move() {

        transform.localPosition = uniformCircularMotionBehaviour.GeneratePosition();

    }
}
