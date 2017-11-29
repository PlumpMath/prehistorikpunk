using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public bool isGameOver;
	public bool isBossDead;
	public GameObject blockers;
	public GameObject continueArrow;
	public GameObject hero;
	public GameObject spawners;
	public GameObject endOfBlocker;
	public GameObject bossUI;
	public bool isPaused;
	public string buttonToTogglePause;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

	}

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameOver) {
			print("GAME OVER!");	
		}
			
		if (Input.GetButtonDown (buttonToTogglePause)) {
			isPaused = !isPaused;
		}
	}

	public void onGameOver() {
		isGameOver = true;
	}

	public void onBossDead() {
		print("BOSS DEAD!");	
		isBossDead = true;
		// spawners.SetActive(true);
		blockers.SetActive(false);
		continueArrow.SetActive(true);
		endOfBlocker.SetActive(false);
		bossUI.SetActive(false);
		Invoke("hideArrow", 5f);
	}

	private void hideArrow() {
		continueArrow.SetActive(false);
	}
}
