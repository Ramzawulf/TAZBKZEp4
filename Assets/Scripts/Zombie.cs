using UnityEngine;
using System.Collections;
using System.Linq;

public class Zombie : MonoBehaviour
{
	
	//Moves per second
	public float movementSpeed = 1;
	private float lastMove = 0;
	private Farmer farmer;
	
	void Start ()
	{
		lastMove = Time.time;
		farmer = GameObject.FindGameObjectsWithTag ("Farmer").First ().GetComponent<Farmer> ();
	}
	
	void Update ()
	{
		if (GameController.control.isPaused || GameController.control.isGameOver) 
			return;
		Move ();
	}
	
	void Move ()
	{
		int horizontalDif = (int)(farmer.transform.position.x - transform.position.x);
		int verticalDif = (int)(farmer.transform.position.y - transform.position.y);
		
		if (Time.time < (lastMove + (1 / movementSpeed)))
			return;
		
		if (horizontalDif != 0) {
			Vector2 targetposition = new Vector2 (transform.position.x + Mathf.Sign (horizontalDif), transform.position.y); 
			rigidbody2D.MovePosition (targetposition);
			transform.rotation = Quaternion.identity;
		} else if (verticalDif != 0) {
			Vector2 targetposition = new Vector2 (transform.position.x, transform.position.y + Mathf.Sign (verticalDif));
			rigidbody2D.MovePosition (targetposition);
			transform.rotation = Quaternion.identity;
		}
		lastMove = Time.time;
		
	}
	
	void OnTriggerEnter (Collider other)
	{
		print ("trigger");
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Farmer") {
			Farmer.control.Damage (1);

		} else if (coll.gameObject.tag == "Enemy") {
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.angularVelocity = 0;
			transform.rotation = Quaternion.identity;

			coll.gameObject.rigidbody2D.velocity = Vector2.zero;
			coll.gameObject.rigidbody2D.angularVelocity = 0;
			coll.gameObject.transform.rotation = Quaternion.identity;
		}
	}
}
