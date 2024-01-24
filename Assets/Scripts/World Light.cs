using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


// All code written by Ronnie

namespace WorldTime {


    [RequireComponent(typeof(Light2D))]

    public class WorldLight : MonoBehaviour
    {
        public float duration = 600f; // The gradient has a duration of 600 seconds/10 minutes

        [SerializeField] private Gradient gradient;
        private Light2D _light;
        private float _startTime;



        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _startTime = Time.time;
        }

        private void Update()
        {
            var timeElapsed = Time.time - _startTime;
            var percentage = Mathf.Sin(f: timeElapsed / duration);
            percentage = Mathf.Clamp01(percentage);
            print(percentage);

            _light.color = gradient.Evaluate(percentage);
        }


    }
}
