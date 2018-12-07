using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;



	public class TitleScene : MonoBehaviour {

		public Text BuildInfoText;

		private void Start() {
		//	SetBuildInfo();
		//	AudioPlayer.Instance.StopBgm();
		}

		private void SetBuildInfo () {
		//	string info = "";
		//	BuildInfoText.text = info;
		}

		public void ActiveZoneOnClick() {
			//GoToScene (Scene.LevelSelect);
		SceneManager.LoadScene ("game3"); 

		}
	}

