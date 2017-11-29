using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {
	public AudioClip victorySound;
	public int delay;
	public GameObject musicSource;
	public string levelToLoad;

	private bool isTimePassed;

	// Update is called once per frame
	void Update () {
		if (Time.time >= delay && !isTimePassed) {			
			SoundController.instance.playOneShot(victorySound);
			musicSource.SetActive(false);
			Invoke("loadLevel", 1.5f);
			isTimePassed = true;
		}
	}

	void loadLevel() {
		SceneManager.LoadScene(levelToLoad);		
	}
}
