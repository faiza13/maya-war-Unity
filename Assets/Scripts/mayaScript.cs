using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mayaScript : MonoBehaviour {
	Animator animator;
	string direction;
	Rigidbody2D m_Rigidbody;
    float m_Speed;
	// Use this for initialization
	bool right ,left ;
	float move;
	float dirX;
	void Start () {

		
		 m_Rigidbody = this.GetComponent<Rigidbody2D>();
        //Set the speed of the GameObject
        m_Speed = 10.0f;
		animator = this.GetComponent<Animator> ();
	//	transform.Rotate (new Vector2 (0, 180f));
		right=true;

		
	}
	
	// Update is called once per frame
	void Update () {
	 move = - Input.GetAxisRaw("Horizontal");

		

		    


		
	}
	public void turnLeft(){
		
      		                                       	    	animator.SetInteger ("state",3);




    

    }
    public void turnRight(){
transform.Translate(Vector3.up *1,0); 

		
		                                       	    	animator.SetInteger ("state",2);

}
public void idle (){
		animator.SetInteger ("state",0);

}
public void idleRight (){
	transform.Translate(Vector3.down *1,0); 

		animator.SetInteger ("state",0);

}

}