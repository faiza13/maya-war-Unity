using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {
  void Start()
    {
    	
    }

	public void PlayGame(){
		SceneManager.LoadScene ("LevelSelect"); 
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
