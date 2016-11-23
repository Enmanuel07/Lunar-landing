using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utilities {
    class UniformCircularMotionBehaviour {

        private float _angle;
        private float _time;

        public float Speed { get; set; }
        public float Acceleration { get; set; }
        public float Radius { get; set; }

        public UniformCircularMotionBehaviour() {

        }

        public UniformCircularMotionBehaviour(float speed, float acceleration, float radius) {

            Speed = speed;
            Acceleration = acceleration;
            Radius = radius;
        }

        public Vector3 GeneratePosition() {

            _time += UnityEngine.Time.deltaTime;

            Speed += Acceleration * (_time);
            _angle = Speed * _time / Radius;

            return new Vector3(Radius * Mathf.Cos(_angle), Radius * Mathf.Sin(_angle));

        }

    }
}
