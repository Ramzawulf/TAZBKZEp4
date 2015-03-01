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
	private Vector3 initialBullScale;

	private bool amIDead = false;
	
	
	void Start ()
	{
		direction = BullDirection.Up;
		lastMovement = Time.time;
		initialBullScale = gameObject.transform.localScale;
	}
	
	void Update ()
	{
		if (amIDead) {
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
		if (Time.time < (lastMovement + (1 / movementSpeed)))
			return;
					
		Vector2 targetPosition = transform.position;

		SetDirection ();


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
		rigidbody2D.MovePosition (targetPosition);
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
			break;
		case "Wall":
			KillMe ();
			break;
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		//KillMe ();
	}

	public void 	KillMe ()
	{
		amIDead = true;
	}
}