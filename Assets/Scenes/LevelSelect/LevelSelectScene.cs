using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



	public class LevelSelectScene : MonoBehaviour {
		
		//public List<LevelSlot> LevelSlots;

		private void Start() {
		//	InitLevelSlots ();
		}

		private void InitLevelSlots() {
			//for (int i = 0; i < LevelSlots.Count; i++) {
			//	LevelSlots[i].Init((i < Session.Instance.GetTotalLevels()), i + 1, LevelSlotOnClick);
			//}
		}

		private void LevelSlotOnClick(int levelId) {
			//Session.Instance.PlayLevel(levelId);
			//GoToScene (Scene.LevelHandler);
		}
		public void LoadLevel1(){
		SceneManager.LoadScene ("game3"); 
	}
	public void LoadLevel2(){
		SceneManager.LoadScene ("game4"); 
	}


	public void QuitGame(){
		Application.Quit ();
	}
	}
	

