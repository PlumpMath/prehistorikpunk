using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	public Transform target;		// Reference to the player.


	void Awake ()
	{
		// Setting up the reference.
		// player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		if (target != null) {
			// Set the position to the player's position with the offset.
			transform.position = target.position + offset;
		}
	}
}
