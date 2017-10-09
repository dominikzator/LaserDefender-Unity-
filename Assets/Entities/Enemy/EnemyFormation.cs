using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyFormation : MonoBehaviour {
	
	public GameObject projectile;
	public float projectileSpeed = 10f;
	
	public float maxhealth = 400f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	public AudioClip enemyDies;
	public Transform[] pos;
	public GameObject healthPack;
	public GameObject HitParticle;
	public GameObject dieParticle;
	private float health;

	public AnimationClip clip;
	private Animator anim;	

	private Transform trans;
	private bool isDead = false;

	void Start()
	{
		//if (!GameObject.Find ("LevelManager").GetComponent<LevelManager> ().AllEnemiesLowHp) {
			health = maxhealth;
			Debug.Log (health);
		//}
		anim = GetComponent<Animator> ();
		trans = GetComponent<Transform> ();
	}
	
	void Update()
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability && !isDead)
		{
			Fire ();
		}
	}
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0f,-1f,0f);
		GameObject missile = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
		gameObject.GetComponent<AudioSource>().Play();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			if (gameObject.name == "EnemyBoss" || gameObject.name == "EnemyBoss(Clone)")
			{
				GenerateHealthPack (95f);
			}
			GameObject effect = Instantiate(HitParticle,gameObject.transform.position,Quaternion.identity) as GameObject;
			effect.transform.parent = gameObject.transform;
			health -= missile.GetDamage();
			UpdateEnemyHealth (health);

			missile.Hit ();
			if (health<=0  && !isDead)
			{
				isDead = true;
				PlayerController.killsWave++;
				GameObject.FindObjectOfType<PlayerController>().UpdateEnemiesLeft();
				
				GenerateHealthPack (80f);
				anim.SetTrigger ("dead");

				GameObject.FindObjectOfType<ScoreKeeper>().Score(scoreValue);
				AudioSource.PlayClipAtPoint(enemyDies, transform.position, 1f);
				Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1f); 

			}
		}
	}
	void UpdateEnemyHealth(float health)
	{
		if (health < 0f) {
			health = 0f;
		}
		float healthValue = health/maxhealth;
		gameObject.transform.GetChild(0).GetChild(0).localScale = new Vector3(healthValue,1f,1f);
		gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1-healthValue,healthValue,0,1);
	}
	void GenerateHealthPack(float probability)
	{
		float random = Random.Range(0f,100f);
		if(random >= probability)
		{
			GameObject healthPackObject = Instantiate(healthPack, transform.position, Quaternion.identity) as GameObject;
		}
	}
}
