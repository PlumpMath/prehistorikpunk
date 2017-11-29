using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPowerUp : MonoBehaviour {
	public float value = 10f;
	public AudioClip SFX;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {			
			PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth>();
			if (ph.health < 100f) {
				SoundController.instance.playOneShot(SFX);
				ph.addHP(value);
				Destroy(gameObject);
			}
		}
	}
}
