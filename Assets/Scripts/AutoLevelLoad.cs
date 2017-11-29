using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLevelLoad : MonoBehaviour {
	public string levelToLoad;
	public float delay = 10f;
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad >= delay) {
			SceneManager.LoadScene(levelToLoad);
		}
	}
}
