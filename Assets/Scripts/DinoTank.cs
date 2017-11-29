using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoTank : MonoBehaviour {
	public Transform target;

	// Update is called once per frame
	void Update () {
		flip(target.position.x < transform.position.x);
	}

	private void flip(bool isFlipped) {
		Quaternion rot = Quaternion.identity;
		if (isFlipped) {
			rot = Quaternion.Euler (0, 0, 0);
		} else {
			rot = Quaternion.Euler (0, 180, 0);
		}

		// gun.rotation = rot;
		transform.rotation = rot;
	}


}
