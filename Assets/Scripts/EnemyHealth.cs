using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public float HP = 100;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public SpriteRenderer healthBar;
	public bool isDead {get {return dead;}}
	public bool isBoss;
	public int scoreValue = 100;

	private Vector3 healthScale;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	
	void Awake() {
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		score = GameObject.Find("Score").GetComponent<Score>();
		if (healthBar != null) {
			healthScale = healthBar.transform.localScale;
		}

	}

	void FixedUpdate () {
		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public void Hurt() {
		// Reduce the number of hit points by one.
		HP--;
		if (healthBar != null && HP > 0) {
			updateHealthBar();
		}
	}
	
	void Death() {
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		if (!isBoss) {
			// Disable all of them sprite renderers.
			foreach(SpriteRenderer s in otherRenderers) {
				s.enabled = false;
			}

			// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
			ren.enabled = true;
			ren.sprite = deadEnemy;
		}


		// Increase the score by 100 points
		score.score += scoreValue;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols) {
			c.isTrigger = true;
		}
		cols = GetComponentsInChildren<Collider2D>();
		foreach(Collider2D c in cols) {
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
		if (isBoss) {			
			GameManager.instance.onBossDead();
		}
	}		

	public void updateHealthBar () {		
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - HP * 0.01f);
		healthBar.transform.localScale = new Vector3(healthScale.x * HP * 0.01f, 1, 1);
	}
}
