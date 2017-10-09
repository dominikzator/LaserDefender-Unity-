using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	
	
	public static int score = 0;	
	private Text myText;
	Vector3 right;
	void Start()
	{
		myText = GetComponent<Text>();
		Reset();
	}
	
	public void Score(int points)
	{
		Debug.Log ("Scored points");
		score += points;
		myText.text = score.ToString();
	}
	
	void Update()	
	{
		right = Camera.main.ViewportToScreenPoint(new Vector3(0.80f, 0.05f, 0));
		myText.transform.position = right;
	}
	
	public static void Reset()
	{
		score = 0;
		//myText.text = score.ToString();
	}
}