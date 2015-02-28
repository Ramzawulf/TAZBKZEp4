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
	
	
	void Start ()
	{
		direction = BullDirection.Up;
		lastMovement = Time.time;
	}
	
	void Update ()
	{
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
			break;
		case BullDirection.Down:
			targetPosition = new Vector2 (transform.position.x, transform.position.y - 1);
			break;
		case BullDirection.Left:
			targetPosition = new Vector2 (transform.position.x - 1, transform.position.y);
			break;
		case BullDirection.Right:
			targetPosition = new Vector2 (transform.position.x + 1, transform.position.y);
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

	void OnTriggerEnter (Collider other)
	{
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		
	}
}