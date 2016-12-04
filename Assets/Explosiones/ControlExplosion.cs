using UnityEngine;
using System.Collections;

public class ControlExplosion : MonoBehaviour
{
    // Use this for initialization
    public AudioSource sonidoExplosion;

    void Start()
    {
        if (!ConfiguracionGlobal.efectos)
            sonidoExplosion.Stop();
        else
            sonidoExplosion.Play();
        Destroy(gameObject, 2f);
    }
}
