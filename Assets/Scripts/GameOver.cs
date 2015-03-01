using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	void Start ()
	{
		
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel ("Main");
		}
	}
}
