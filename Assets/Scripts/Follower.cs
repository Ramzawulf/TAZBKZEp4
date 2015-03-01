using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{

	public Follower follower;
	public GameObject leader;
	public float followerDelay = 0.3f;
	private Vector3 lastPosition;
	
	private Vector2 targetPosition;

	void Awake ()
	{
		lastPosition = transform.position;
	}

	void Update ()
	{
		if (leader == null)
			Destroy (gameObject);
	}

	public void FollowMe (Vector2 LeaderPosition)
	{
		lastPosition = transform.position;
		rigidbody2D.MovePosition (LeaderPosition);
		if (follower != null) {
			follower.FollowMe (lastPosition);
		}
	}

	public void KillMe ()
	{
		if (follower != null) {
			follower.KillMe ();
		}
		Destroy (gameObject, 0.5f);
	}

	public void SpawnCow ()
	{
		if (follower == null) {
			GameObject cow = Instantiate (gameObject, lastPosition, Quaternion.identity) as GameObject;
			follower = cow.GetComponent<Follower> ();
			follower.leader = gameObject;
		} else {
			follower.SpawnCow ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") {
			GameController.control.score++;
			Destroy (other.gameObject);
			KillMe ();
		}
	}

}
