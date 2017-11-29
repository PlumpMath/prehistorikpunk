using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1 : MonoBehaviour {
	public Transform target;
	public float moveSpeed = 1.5f;
	public float jumpForce = 5f;
	public bool isJump;
	public bool isAttack = false;
	public float attackDuration = 5f;
	public Transform laserOrigin;
	public AudioClip laserSfx;
	public GameObject laserEye;
	public int HP = 500;
	public Transform gun;

	private Rigidbody2D rb2d;
	private Transform frontCheck;
	private bool isGrounded;
	private Transform crossObstacle;
	private Transform groundCheck;
	private Coroutine laserAttackRoutine;
	private Collider2D firePositionCollider;
	private LineRenderer lineRenderer;
	private bool isLaserAttack;
	private bool isWalkLaserAttack;
	private SpriteRenderer healthBar;
	private EnemyHealth enemyHealth;

	private enum State {
		walk,
		attack
	};

	// Use this for initialization
	void Start () {		
		rb2d = GetComponent<Rigidbody2D>();
		frontCheck = transform.Find("frontCheck").transform;
		groundCheck = transform.Find("groundCheck");
		lineRenderer = laserOrigin.GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		enemyHealth = GetComponent<EnemyHealth>();
		// healthBar = GameObject.Find("BossHealthBar").GetComponent<SpriteRenderer>();
		// StartCoroutine(walkLaserAttack ());
	}

	void Update() {
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		if (enemyHealth.isDead) {
			stopAttack();
			return;
		}
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

		foreach(Collider2D c in frontHits) {	
			if (c.tag == "BossFirePosition" && !isAttack) {								
				firePositionCollider = c;
				isAttack = true;
				c.enabled = false;
			}
			if (c.tag == "crossObstacle") {
				crossObstacle = c.transform;
				isJump = true;
			}
			// If any of the colliders is an Obstacle...
			if(c.tag == "Obstacle") {
				// ... Flip the enemy and stop checking the other colliders.
				Flip();
				break;
			}

		}
			

		// RaycastHit2D hit = Physics2D.Raycast(frontCheck.position, transform.TransformDirection(Vector2.left));
		// Debug.DrawRay(frontCheck.position, transform.TransformDirection(Vector2.left) * 2, Color.green);

		if (isAttack) {			
			
			// RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, -Vector2.left, Mathf.Infinity);
			// Debug.DrawRay(laserOrigin.position, -hit.point, Color.red);
			if (!isLaserAttack) {
				laserEye.SetActive(true);
				if (laserAttackRoutine == null)
					laserAttackRoutine = StartCoroutine(laserAttack());
				
				isWalkLaserAttack = false;				
				lineRenderer.enabled = true;
				Invoke("stopAttack", attackDuration);
				isLaserAttack = true;
			}				

			rb2d.velocity = Vector2.zero;
		} else {				
			rb2d.velocity = new Vector2(transform.localScale.x * moveSpeed, rb2d.velocity.y);

			if (isJump) {
				// rb2d.velocity = new Vector2(((crossObstacle.position.x - 200f) * jumpSpeed) * Time.deltaTime, ((crossObstacle.position.y * 2f) * jumpSpeed) * Time.deltaTime);
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-jumpForce, jumpForce));
				isJump = false;
			}
		}


	}

	public void Flip() {
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
		// gun is rotating
		gun.localScale = enemyScale;
	}				

	private void stopAttack() {
		isAttack = false;
		isLaserAttack = false;
		Invoke("resetFirePosition", 1f);
		if (laserAttackRoutine != null) {
			StopCoroutine(laserAttackRoutine); // doesn't work
		}

		lineRenderer.enabled = false;
		laserEye.SetActive(false);
	}

	private void resetFirePosition() {
		firePositionCollider.enabled = true;
	}

	private IEnumerator laserAttack(){
		// isLaserAttack = true;
		yield return new WaitForSeconds(0.1f);
		if (isLaserAttack && !enemyHealth.isDead) {

			RaycastHit2D[] hits = Physics2D.RaycastAll(laserOrigin.position, Vector2.left);
			foreach(RaycastHit2D rayHit in hits) {
				Debug.Log ("---" + rayHit.collider.tag);
				if (rayHit.collider != null && rayHit.collider.tag == "Player") {
					print("**************************");
				}
			}

			RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, Vector2.left, Mathf.Infinity);
			lineRenderer.SetPosition(0, laserOrigin.position);
			// lineRenderer.SetPosition(1, new Vector3((laserOrigin.position.x + 1f) * -1, hit.point.y, -10f));
			lineRenderer.SetPosition(1, new Vector3(hit.point.x * -1, Random.Range(-100, 60), -10f));
			SoundController.instance.playOneShot(laserSfx);


			// print (hit.collider);
			// if (hit!= null && hit.collider.tag == "Player") {
				// print("hit player");
			// }			
		}


		yield return StartCoroutine(laserAttack());
	}

	private IEnumerator walkLaserAttack(){		
		
		yield return new WaitForSeconds(3f);
		if (!isLaserAttack) {
			RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, -Vector2.left, Mathf.Infinity);
			rb2d.velocity = Vector2.zero;
			lineRenderer.SetPosition(0, laserOrigin.position);
			lineRenderer.SetPosition(1, new Vector3(laserOrigin.position.x * -1, hit.point.y, -10f));
			// lineRenderer.SetPosition(1, new Vector3(hit.point.x * -1, Random.Range(-100, 60), -10f));

			if (hit.collider.tag == "Player") {
				print("hit player");
			}			
		}


		yield return StartCoroutine(walkLaserAttack());
	}
}
