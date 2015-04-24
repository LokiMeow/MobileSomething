using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class StaticParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Pause", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Pause()
    {
        transform.GetComponent<ParticleSystem>().Pause();
    }
}
