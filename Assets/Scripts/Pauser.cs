using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	public GameObject pausePanel;

	private bool paused = false;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape)) {
			paused = !paused;
			if (paused) {
				pausePanel.SetActive (true);
			} else {
				pausePanel.SetActive (false);
			}
		}

		if (paused) {			
			Time.timeScale = 0;
		} else {			
			Time.timeScale = 1;
		}
			
	}
}
