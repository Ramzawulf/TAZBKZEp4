using UnityEngine;
using System.Collections;

public class Bull : MonoBehaviour
{
	[SerializeField]
	public enum BullDirection
	{
		Up, 
		Down,
		Left,
		Right
	}
	
	//Moves per second
	public float movementSpeed = 1;
	public BullDirection direction;

	private float lastMovement;
	private Vector3 lastPosition;

	public GameObject cow;
	private Follower follower;

	private bool amIDead = false;
	
	
	void Start ()
	{
		direction = BullDirection.Up;
		lastMovement = Time.time;
		lastPosition = transform.position;
	}
	
	void Update ()
	{
		if (amIDead || GameController.control.isGameOver) {
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.angularVelocity = 0;
			return;
		}

		if (GameController.control.isPaused) 
			return;
		Move ();
	}
	
	void Move ()
	{			
		Vector2 targetPosition = transform.position;

		SetDirection ();

		if (Time.time < (lastMovement + (1 / movementSpeed)))
			return;

		switch (direction) {
		case BullDirection.Up:
			targetPosition = new Vector2 (transform.position.x, transform.position.y + 1);
			transform.rotation = Quaternion.identity;
			break;
		case BullDirection.Down:
			targetPosition = new Vector2 (transform.position.x, transform.position.y - 1);
			transform.rotation = Quaternion.identity;
			transform.Rotate (new Vector3 (0, 0, 180));
			break;
		case BullDirection.Left:
			targetPosition = new Vector2 (transform.position.x - 1, transform.position.y);
			transform.rotation = Quaternion.identity;
			transform.Rotate (new Vector3 (0, 0, 90));
			break;
		case BullDirection.Right:
			targetPosition = new Vector2 (transform.position.x + 1, transform.position.y);
			transform.rotation = Quaternion.identity;
			transform.Rotate (new Vector3 (0, 0, 270));
			break;
		}

		lastMovement = Time.time;
		lastPosition = transform.position;
		rigidbody2D.MovePosition (targetPosition);
		if (follower != null) {
			follower.FollowMe (lastPosition);
		} 
	}

	private void SetDirection ()
	{
		int horizontal = (int)Input.GetAxisRaw ("BullHorizontal");
		int vertical = (int)Input.GetAxisRaw ("BullVertical");

		if (horizontal > 0 && direction != BullDirection.Left) {
			direction = BullDirection.Right;
		} else if (horizontal < 0 && direction != BullDirection.Right) {
			direction = BullDirection.Left;
		} else if (vertical > 0 && direction != BullDirection.Down) {
			direction = BullDirection.Up;
		} else if (vertical < 0 && direction != BullDirection.Up) {
			direction = BullDirection.Down;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		switch (other.tag) {
		case"Enemy":
			Destroy (other.gameObject);
			GameController.control.score++;
			if (follower == null) {
				GameObject c = Instantiate (cow, lastPosition, Quaternion.identity) as GameObject;
				follower = c.GetComponent<Follower> ();
				follower.leader = gameObject;
			} else {
				follower.SpawnCow ();
			}

			break;
		case "Wall":
			KillMe ();
			break;
		case "Cow":
			KillMe ();
			break;
		}
	}

	private IEnumerable SpawnFollower ()
	{
		if (follower == null) {
			Vector2 currentPosition = transform.position;
			yield return new WaitForSeconds (0.3f);
			GameObject c = Instantiate (cow, currentPosition, Quaternion.identity) as GameObject;
			follower = c.GetComponent<Follower> ();
		} else {
			follower.SpawnCow ();
			yield return null;
		}

	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		//KillMe ();
	}

	public void 	KillMe ()
	{
		GameController.control.GameOver ();
		amIDead = true;
	}
}