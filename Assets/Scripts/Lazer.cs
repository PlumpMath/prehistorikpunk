using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
	private Rigidbody2D rb2d;
	public GameObject explosion;
	public float force = 100f;
	public float damage = 10f;
	public int ttl = 4;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		// Destroy the lazer after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, ttl);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth>().takeHit(damage);
			onExplode();
			Destroy (gameObject);
		}
	}

	void onExplode() {
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void FixedUpdate() {
		rb2d.velocity = transform.right * force;
	}
}
