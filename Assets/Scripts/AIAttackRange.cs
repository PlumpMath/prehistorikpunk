using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackRange : MonoBehaviour {
	public Transform target;
	public Transform attackOrigin;
	public float attackRange;
	public int damage;
	public float attackDelay;
	public int rotationSpeed = 10;
	public GameObject lazer;
	public float lazerForce;
	public AudioClip laserSfx;

	private float lastAttackTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 targetDir = target.position - transform.position;
			float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 12f;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);

			if (Time.time > lastAttackTime + attackDelay) {		
				transform.position = new Vector2(transform.position.x -0.5f, transform.position.y);
				Invoke("resetRecoil", 0.1f);
				Instantiate(lazer, attackOrigin.position, attackOrigin.rotation);	
				SoundController.instance.playOneShot(laserSfx);
				lastAttackTime = Time.time;

			}
		}
	}

	void resetRecoil() {
		transform.position = new Vector2(transform.position.x +0.5f, transform.position.y);
	}
}
