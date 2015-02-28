using UnityEngine;
using System.Collections;

public class Farmer : MonoBehaviour
{

	//Moves per second
	public static Farmer control;
	public float movementSpeed = 1;
	public int lifePoints = 1;

	private float lastMove = 0;
	private bool amIDead = false;

	void Awake ()
	{
		if (control == null) {
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		lastMove = Time.time;
	}

	void Update ()
	{
		if (amIDead) {
			PlayDead ();
			return;
		}

		if (GameController.control.isPaused) 
			return;
		Move ();
	}

	void Move ()
	{
		int horizontal = (int)Input.GetAxisRaw ("Horizontal");
		int vertical = (int)Input.GetAxisRaw ("Vertical");

		if (Time.time < (lastMove + (1 / movementSpeed)))
			return;
			
		if (horizontal != 0) {
			Vector2 targetposition = new Vector2 (transform.position.x + horizontal, transform.position.y);
			rigidbody2D.MovePosition (targetposition);
			transform.rotation = Quaternion.identity;
		} else if (vertical != 0) {
			Vector2 targetposition = new Vector2 (transform.position.x, transform.position.y + vertical);
			rigidbody2D.MovePosition (targetposition);
			transform.rotation = Quaternion.identity;
		}
		lastMove = Time.time;
	}

	public void Damage (int damageDone = 1)
	{
		lifePoints -= damageDone;
		CheckIfAlive ();
	}

	private void CheckIfAlive ()
	{
		if (lifePoints <= 0) {
			GameController.control.GameOver ();
			amIDead = true;
		}
	}

	void KillMe ()
	{
		amIDead = true;
		print ("Hilly killed!");
	}

	void PlayDead ()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		transform.rotation = Quaternion.identity;
	}
}