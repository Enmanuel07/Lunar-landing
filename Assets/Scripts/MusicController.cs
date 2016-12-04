using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

    //public AudioMixerSnapshot city;
    //public AudioMixerSnapshot victory;
    //public AudioMixerSnapshot almostThere;
    public AudioSource city;
    public AudioSource victory;
    public AudioSource almostThere;
    private Transform _platform;
    public GameObject musicPlayer;
    private bool _landed;
	// Use this for initialization
	void Start () {
        _landed = false;
	}

    void Update() {

        if(_platform == null) {
            _platform = GameObject.FindGameObjectWithTag("Plataforma").transform;
        } else {
            if (!_landed && (Vector3.Distance(_platform.position, transform.position) < 10)) {

                if (!almostThere.isPlaying) {
                    city.Stop();
                    almostThere.Play();
                }

                //almostThere.TransitionTo(1);
            } else {

                if(!_landed && !city.isPlaying) {
                    //city.TransitionTo(1);
                    almostThere.Stop();
                    city.Play();
                }
                
            }
        }

        //Debug.Log(Mathf.Abs((_platform.transform.position - transform.position).magnitude));

        //if (Mathf.Abs((_platform.transform.position - transform.position).magnitude) < 20.0f) {

        //    almostThere.TransitionTo(1);
            
        //} else {
        //    city.TransitionTo(1);
        //}

    }
	
    void OnCollisionEnter(Collision other) {
              

        if(other.gameObject.CompareTag("Plataforma")) {
            
            if (other.relativeVelocity.sqrMagnitude <= 4) {

                _landed = true;

                //victory.TransitionTo(2);
                almostThere.Stop();
                victory.Play();
            }
        }
    }
	
}
