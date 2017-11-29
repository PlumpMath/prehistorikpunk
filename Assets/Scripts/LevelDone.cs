using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDone : MonoBehaviour {
	public string levelToLoad;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			if (levelToLoad == "Exit") {
				Application.Quit ();
			} else {
				SceneManager.LoadScene(levelToLoad);
			}
		}
	}
}
