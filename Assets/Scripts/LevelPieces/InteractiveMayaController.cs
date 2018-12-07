using UnityEngine;
using System.Collections;


	public class InteractiveMayaController : MonoBehaviour { 

	//	public AudioClip CoinPickupFx;
		public delegate void StartInteractionDelegate();
		public static event StartInteractionDelegate StartInteractionEvent;

		private void OnTriggerEnter2D(Collider2D col) {
			if(StartInteractionEvent != null) {
				StartInteractionEvent();
			}
			
		}

	}


