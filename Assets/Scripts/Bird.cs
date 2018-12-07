using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
	public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;

    // Use this for initialization
    void Start()
    {

        //trailrenderer is not visible until we throw the bird
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
        //no gravity at first
        GetComponent<Rigidbody2D>().isKinematic = true;
        //make the collider bigger to allow for easy touching
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;
        State = BirdState.BeforeThrown;
    }



    void FixedUpdate()
    {
        //if we've thrown the bird
        //and its speed is very small
        if (State == BirdState.Thrown &&
            GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
        {
            //destroy the bird after 2 seconds
            StartCoroutine(DestroyAfter(0.5f));
        }
    }
    void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag != "Bird" && col.gameObject.tag != "Maya")
		{ 
		  AudioSource audio = gameObject.AddComponent<AudioSource >();
        audio.PlayOneShot((AudioClip)Resources.Load("explosion_short_blip_rnd_01"));
		 GameObject foo = GameObject.Instantiate((GameObject)Resources.Load("Explosion"));
		 foo.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
}

}

    public void OnThrow()
    {
    	Animator	animator = GameObject.FindGameObjectWithTag("maya").GetComponent<Animator> ();
					animator.SetInteger ("state",1);
        AudioSource audio = gameObject.AddComponent<AudioSource >();
        audio.PlayOneShot((AudioClip)Resources.Load("DM-CGS-06"));
        //play the sound
        GetComponent<AudioSource>().Play();
        //show the trail renderer
        GetComponent<TrailRenderer>().enabled = true;
        //allow for gravity forces
        GetComponent<Rigidbody2D>().isKinematic = false;
        //make the collider normal size
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;
        State = BirdState.Thrown;
        if(StartInteractionEvent != null) {
				StartInteractionEvent();
			}
    }

    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public BirdState State
    {
        get;
        private set;
    }
}
