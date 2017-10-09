using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public GameObject HealthBar;
	public float width = 10f;
	public float height = 5f;
	public float speed = 0.1f;
	public float padding = 1f;
	public float spawnDelay = 0.5f;
	//public Transform pos;	
	public int enemiesNumber = 0;
	public GameObject waveCleared;
	public GameObject bossInfo;
	public GameObject levelCleared;
	public Canvas canvas;
	public Text wave;
	public Text level;
	
	
	private Vector3 leftmost;
	private Vector3 rightmost;
	float direction = 1;
	float xmin;
	float xmax;
	// Use this for initialization
	void Start () {	
		SpawnUntilFull();
		canvas = GameObject.FindObjectOfType<Canvas>();
		float distance = transform.position.z - Camera.main.transform.position.z;
		leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,distance));
		rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f,0f,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(direction*speed*Time.deltaTime,0f,transform.position.z);
		
		if(transform.position.x>=rightmost.x - this.width/2 )
		{
			direction = -1;
		}
		else if(transform.position.x<=leftmost.x + this.width/2)
		{
			direction = 1;
		}
		
		if(AllMembersDead())
		{
			if (GameObject.FindObjectOfType<PlayerController> ().waveNumber == 0) {
				GameObject.FindObjectOfType<PlayerController> ().shootNumber = 2;
				GameObject.FindObjectOfType<PlayerController> ().waveNumber++;
				GameObject.FindObjectOfType<PlayerController> ().UpdateWaveNumber (GameObject.FindObjectOfType<PlayerController> ().waveNumber + 1);
				GameObject.FindObjectOfType<LevelManager> ().GenerateWave (GameObject.FindObjectOfType<PlayerController> ().waveNumber);
				GameObject waveClearInfo = Instantiate (waveCleared, canvas.transform.position, Quaternion.identity) as GameObject;
				waveClearInfo.transform.SetParent (canvas.transform, false);

			} else if (GameObject.FindObjectOfType<PlayerController> ().waveNumber == 1) {
				GameObject.FindObjectOfType<PlayerController> ().shootNumber = 3;
				GameObject.FindObjectOfType<PlayerController> ().waveNumber++;
				GameObject.FindObjectOfType<PlayerController> ().UpdateWaveNumber (GameObject.FindObjectOfType<PlayerController> ().waveNumber + 1);
				GameObject.FindObjectOfType<LevelManager> ().GenerateWave (GameObject.FindObjectOfType<PlayerController> ().waveNumber);
				GameObject waveClearInfo = Instantiate (waveCleared, canvas.transform.position, Quaternion.identity) as GameObject;
				waveClearInfo.transform.SetParent (canvas.transform, false);

			} else if (GameObject.FindObjectOfType<PlayerController> ().waveNumber == 2) {
				
				GameObject.FindObjectOfType<PlayerController> ().waveNumber++;
				GameObject.FindObjectOfType<PlayerController> ().UpdateWaveNumber (GameObject.FindObjectOfType<PlayerController> ().waveNumber + 1);
				GameObject.FindObjectOfType<LevelManager> ().GenerateWave (GameObject.FindObjectOfType<PlayerController> ().waveNumber);
				GameObject bossNearInfo = Instantiate (bossInfo, canvas.transform.position, Quaternion.identity) as GameObject;
				bossNearInfo.transform.SetParent (canvas.transform, false);
				//wave.text = "Boss fight";
			} else if (GameObject.FindObjectOfType<PlayerController> ().waveNumber == 3)
			{
				GameObject.FindObjectOfType<PlayerController> ().waveNumber++;
				GameObject.FindObjectOfType<PlayerController> ().UpdateWaveNumber (GameObject.FindObjectOfType<PlayerController> ().waveNumber + 1);
				GameObject.FindObjectOfType<LevelManager> ().GenerateWave (GameObject.FindObjectOfType<PlayerController> ().waveNumber);
				GameObject LevelClearedInfo = Instantiate (levelCleared, canvas.transform.position, Quaternion.identity) as GameObject;
				LevelClearedInfo.transform.SetParent (canvas.transform, false);
				level.text = "Level: 2";
				Debug.Log("TEST");
				//wave.text = "Boss fight";
			}
			else if (GameObject.FindObjectOfType<PlayerController> ().waveNumber == 4)
			{
				GameObject.FindObjectOfType<PlayerController> ().waveNumber++;
				GameObject.FindObjectOfType<PlayerController> ().UpdateWaveNumber (GameObject.FindObjectOfType<PlayerController> ().waveNumber + 1);
				//GameObject.FindObjectOfType<LevelManager> ().GenerateWave (GameObject.FindObjectOfType<PlayerController> ().waveNumber);
				GameObject waveClearInfo = Instantiate (waveCleared, canvas.transform.position, Quaternion.identity) as GameObject;
				waveClearInfo.transform.SetParent (canvas.transform, false);
				//wave.text = "Boss fight";
			
			}
			enemiesNumber = 0;
			Destroy(gameObject);

			SpawnUntilFull();
			PlayerController.killsWave = 0;
			GameObject.FindObjectOfType<PlayerController>().UpdateEnemiesLeft();	
		}
	}
	
	void SpawnEnemies()
	{
		foreach (Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if(freePosition && enemiesNumber<transform.childCount)
		{
			Vector3 offset = new Vector3(-2f,-4f,0f);
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			//enemy.transform.position = offset;
			//pos = freePosition.transform;
			enemy.transform.parent = freePosition;
			enemiesNumber++;
			GameObject health = Instantiate(HealthBar, freePosition.position+ new Vector3(0f,0.5f,0f), Quaternion.identity) as GameObject;
			health.transform.parent = enemy.transform;
		}
		if(NextFreePosition())
		{
		Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	Transform NextFreePosition()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
}
