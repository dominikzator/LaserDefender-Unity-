using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public GameObject[] wave;

	public GameObject enemyOrange;
	public GameObject enemyGreen;
	public GameObject enemyBlue;
	public GameObject enemyBoss;

	public bool AllEnemiesLowHp = false;

	
	void Start()
	{
		Instantiate(wave[0],new Vector3(0f,3f,0f),Quaternion.identity);
		if (AllEnemiesLowHp) {
			//enemyOrange.GetComponent<EnemyFormation> ().health = 150;
			//enemyGreen.GetComponent<EnemyFormation> ().health = 150;
			//enemyBlue.GetComponent<EnemyFormation> ().health = 150;
			//enemyBoss.GetComponent<EnemyFormation> ().health = 150;
			//
			//enemyOrange.GetComponent<EnemyFormation> ().maxhealth = 150;
			//enemyGreen.GetComponent<EnemyFormation> ().maxhealth = 150;
			//enemyBlue.GetComponent<EnemyFormation> ().maxhealth = 150;
			//enemyBoss.GetComponent<EnemyFormation> ().maxhealth = 150;

		}
	}
	
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void GenerateWave(int number)
	{
		Instantiate(wave[number],new Vector3(0f,2.5f,0f),Quaternion.identity);
	}

}
