using UnityEngine;
using System.Collections;

public class HitExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log ("change sorting layer from script");
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Explosions";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
