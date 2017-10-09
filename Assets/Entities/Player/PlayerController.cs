using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public float speed = 15f;
	public float padding = 1f;		
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;	
	public static float maxHealth = 1050f;
	public static float health ;
	public Text level;
	public Text wave;
	public Text enemiesLeft;
	public GameObject playerHealth;
	public int levelNumber = 1;
	public int waveNumber = 0;
	public static int killsWave=0;
	public int shootNumber = 1;
	
	public Camera cam;
	
	private Vector3 screenPos;
	
	float xmin;
	float xmax;
	float ymin;
	float ymax;
	Vector3 leftmost;
	Vector3 rightmost;
	Vector3 upmost;
	Vector3 downmost;
	float distance;
	
	
	void Start()
	{
		health = maxHealth;
		distance = transform.position.z - Camera.main.transform.position.z;
		leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,distance));
		rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f,0f,distance));
		
		downmost = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.05f,distance));
		upmost = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.8f,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		ymin = downmost.y + padding;
		ymax= upmost.y - padding;
		health = maxHealth;
		level.text = "Level: " + levelNumber.ToString();
		wave.text = "Wave: 1/3";
		Vector3 viewPos = cam.WorldToViewportPoint(wave.transform.position);
		UpdateEnemiesLeft();
	}
	
	void Fire()
	{
		Vector3 offset = new Vector3(0f,1f,0f);
		if(shootNumber == 1)
		{
			
			GameObject beam = Instantiate (projectile, transform.position+offset, Quaternion.identity) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			gameObject.GetComponent<AudioSource>().Play();
		}
		else if(shootNumber == 2)
		{
			Vector3 shootVector = new Vector3(0.1f,0f,0f);
			GameObject beam1 = Instantiate (projectile, transform.position+offset+shootVector, Quaternion.identity) as GameObject;
			beam1.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			GameObject beam2 = Instantiate (projectile, transform.position+offset-shootVector, Quaternion.identity) as GameObject;
			beam2.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			gameObject.GetComponent<AudioSource>().Play();
		}
		else if(shootNumber == 3)
		{
			Vector3 shootVector = new Vector3(0.1f,0f,0f);
			GameObject beam1 = Instantiate (projectile, transform.position+offset+shootVector, Quaternion.identity) as GameObject;
			beam1.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			GameObject beam2 = Instantiate (projectile, transform.position+offset-shootVector, Quaternion.identity) as GameObject;
			beam2.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			GameObject beam3 = Instantiate (projectile, transform.position+offset, Quaternion.identity) as GameObject;
			beam3.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,projectileSpeed,0f);
			gameObject.GetComponent<AudioSource>().Play();
		}
	}
	 
	// Update is called once per frame
	void Update () {
		Vector3 helpVector = Camera.main.ViewportToScreenPoint(new Vector3(0.02f, 1f, 0));
		Vector3 offset = new Vector3(2f,0.5f,0f);
		level.transform.position = helpVector;
		wave.transform.position = helpVector + new Vector3(0f,-20f,0f);
		enemiesLeft.transform.position = helpVector + new Vector3(0f,-40f,0f);
		
		playerHealth.transform.position = leftmost + offset;
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp (KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;			
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;		
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;		
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;		
		}
		
		//restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x,xmin,xmax);
		float newY = Mathf.Clamp(transform.position.y,ymin,ymax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
	public void UpdateHealth(float health)
	{
		float healthValue = health/maxHealth;
		playerHealth.transform.GetChild(0).localScale = new Vector3(healthValue,1f,1f);
		playerHealth.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1-healthValue,healthValue,0,1);
	}
	
	public void UpdateWaveNumber(int number)
	{
		if (number == 4) {
			wave.text = "Boss fight";
		}
		else if (number == 5 || number == 6 || number == 7)
		{
			wave.text = "Wave: " + (number - 4).ToString() + "/3"; 
		}
		else
		{
			wave.text = "Wave: "+ number +"/3";
		}
		if (number == 5) {
			level.text = "Level 2";
		}
	}
	public void UpdateEnemiesLeft()
	{
		enemiesLeft.text = "Enemies left: " +  (GameObject.FindObjectOfType<FormationController>().transform.childCount - killsWave).ToString();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{	
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			health -= missile.GetDamage();
			missile.Hit ();
			UpdateHealth(health);
			Debug.Log (health);
			if (health<=0)
			{
				killsWave = 0;
				LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
				man.LoadLevel("Win Screen");
				Destroy(gameObject);
			}
		}
	}
}
