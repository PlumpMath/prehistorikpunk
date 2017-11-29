using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public static SoundController instance;
	public AudioSource sfxSrc;


	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);


	}

	public void playOneShot(AudioClip clip) {
		sfxSrc.PlayOneShot(clip);
	}
}
