using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	void Awake(){
		Cursor.lockState = CursorLockMode.Confined;
	}
	public void PlayGame(){
		SceneManager.LoadScene (1);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
