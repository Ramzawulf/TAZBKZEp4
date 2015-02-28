using UnityEngine;
using System.Collections;

public class Farmer : MonoBehaviour
{

			//Moves per second
			public float movementSpeed = 1;
			private float lastMove = 0;


			void Start ()
			{
						lastMove = Time.time;
			}

			void Update ()
			{
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

			void OnTriggerEnter (Collider other)
			{
						print ("trigger");
			}

			void OnCollisionEnter2D (Collision2D coll)
			{
						print ("Choqué");
						
			}
}