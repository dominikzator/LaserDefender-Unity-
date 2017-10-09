using UnityEngine;
using System.Collections;

public class WaveCleared : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject,4.5f);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{

	}
}
