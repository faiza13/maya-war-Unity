using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ennemymaya : MonoBehaviour {

	public float Health = 150f;
	public int score;
	public Sprite SpriteShownWhenHurt;
	private float ChangeSpriteHealth;
	// Use this for initialization

	Animator animator;
	string direction;
	Text test;
public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;


	// Use this for initialization
	void Start()
	{
		/*test=GameObject.FindWithTag("scoreButton").GetComponentInChildren<Text> () as Text;
		test.text = "Health: " + PlayerPrefs.GetInt ("Health");




		Button	button=GameObject.FindWithTag("score").GetComponent<Button>();

		button.onClick.AddListener(TaskOnClick);

*/


		animator = this.GetComponent<Animator> ();


	}

	void Update () {
		
	}
	void TaskOnClick()
	{
	// 	SceneManager.LoadScene ("game2");
	}


	void OnCollisionEnter2D(Collision2D col)
	{


		//if we are hit by a bird
		if (col.gameObject.tag == "Bird")
		{ 
			 
			animator.SetInteger ("state",5);


			GetComponent<AudioSource>().Play();
			Destroy (col.gameObject);
			 if(StartInteractionEvent != null) {
				StartInteractionEvent();
			}


		/*	//calculate the damage via the hit object velocity
			int damage = (int)col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude ;
			//int damage =10;



			PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt ("Health")-(damage));
		 score=PlayerPrefs.GetInt ("Health");
			if (score <= 0) {

				test.text = "Health: " + "0";

			} else {
				test.text = "Health: " + PlayerPrefs.GetInt ("Health");


			}

			Health -= damage;
			//don't play sound for small damage
			if (damage >= 10)
				GetComponent<AudioSource>().Play();



			if (PlayerPrefs.GetInt ("Health") <= 0) {

				Destroy (this.gameObject);
				//SceneManager.LoadScene ("game2");


			}*/
		}

	}

	//sound found in
	//https://www.freesound.org/people/yottasounds/sounds/176731/
}
