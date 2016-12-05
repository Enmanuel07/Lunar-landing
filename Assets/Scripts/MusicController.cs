using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

    public AudioSource city;
    public AudioSource victory;
    public AudioSource almostThere;
    public AudioSource volcano;
    public GameObject ship;

    private ControlNave _controlNave;
    private ControlJuego _controlJuego;


    private bool _landed;
	// Use this for initialization
	void Start () {
        _controlJuego = Camera.main.GetComponent<ControlJuego>();
        _controlNave = ship.GetComponent<ControlNave>();
    }

    void Update() {

        if(!ConfiguracionGlobal.musica)
        {
            city.Stop();
            volcano.Stop();
            victory.Stop();
            almostThere.Stop();
        }
        if (!_controlNave.Landed() && _controlNave.IsCloseToPlatform()) {
                        
            if (!almostThere.isPlaying) {

                switch (_controlJuego.GetCurrentLevel()) {
                    case Assets.Utilities.Level.None:
                        break;
                    case Assets.Utilities.Level.City:
                        city.Stop();
                        break;
                    case Assets.Utilities.Level.Volcano:
                        volcano.Stop();
                        break;
                    default:
                        break;
                }

                almostThere.Play();
            }

        } else if (_controlNave.Landed() && !victory.isPlaying) {

            almostThere.Stop();
            victory.Play();

        } else {

            if(!_controlNave.Landed()) {

                if (victory.isPlaying) {
                    victory.Stop();
                }

                switch (_controlJuego.GetCurrentLevel()) {
                    case Assets.Utilities.Level.None:
                        break;
                    case Assets.Utilities.Level.City:
                        if (!city.isPlaying) {
                            city.Play();
                        }
                        break;
                    case Assets.Utilities.Level.Volcano:
                        if (!volcano.isPlaying) {
                            volcano.Play();
                        }
                        break;
                    default:
                        break;
                }
                almostThere.Stop();
            }
                
        }
        
    }
	
    //void OnCollisionEnter(Collision other) {
              

    //    if(other.gameObject.CompareTag("Plataforma")) {
            
    //        if (other.relativeVelocity.sqrMagnitude <= 4) {

    //            _landed = true;

    //            almostThere.Stop();
    //            victory.Play();
    //        }
    //    }
    //}
	
}
