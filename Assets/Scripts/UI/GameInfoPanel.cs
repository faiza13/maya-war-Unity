using UnityEngine;
using UnityEngine.UI;
using System.Collections;


	
	public class GameInfoPanel : MonoBehaviour {

		public Text LivesValue;
		public Text ScoreValue;
		public Text HealthValue;

		public void SetLives(int lives) {
			LivesValue.text = lives.ToString();
		}

		public void SetScore(int score) {
			ScoreValue.text = score.ToString("#,##0");
		}
		public void SetHealth(int health) {
			HealthValue.text = health.ToString();
		}


		
	
}