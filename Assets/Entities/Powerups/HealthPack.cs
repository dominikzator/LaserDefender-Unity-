using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {
	
	public float healValue = 250;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name == "Player")
		{
			PlayerController.health += healValue;
			if(PlayerController.health >= PlayerController.maxHealth)
			{
				PlayerController.health = PlayerController.maxHealth;
			}
			GameObject.FindObjectOfType<PlayerController>().UpdateHealth(PlayerController.health);
			Destroy(gameObject);
		}
	}
}
