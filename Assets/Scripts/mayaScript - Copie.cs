using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mayaScriptGame3 : MonoBehaviour {
	Animator animator;
	string direction;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		transform.Rotate (new Vector2 (0, 180f));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
