using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	public static GameController control;
	
	public int width = 19;
	public int height = 19;
	//Floor
	public GameObject floorTile;
	public Transform floorContainer;

	//Walls
	public GameObject wallTile;
	public Transform wallContainer;

	//Farmer
	public Vector2 farmerSpawnPosition = Vector2.zero;
	public GameObject Farmer;

	//Bull
	public Vector2 bullSpawnPosition = new Vector2 (1, 1);
	public GameObject Bull;

	//Zombie
	public int numberOfSpawningPoints = 10;
	public GameObject spawningPoint;
	public Transform spawnPointContainer;

	//Controlling Variables
	public bool isPaused = false;
	public bool isGameOver = false;

	//Score Crap
	public int score;
	public Text scoreText;

	void Awake ()
	{
		PaintMyGridYall ();
		SpawnFarmer ();
		SpawnBull ();
		CreateSpawningPoints ();

		if (control == null) {
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}
	
	void Update ()
	{
		UpdateScore ();

		if (Input.GetKeyDown (KeyCode.P)) {
			isPaused = !isPaused;
		}

		if (isPaused || isGameOver) 
			return;
	}
	
	public void PaintMyGridYall ()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject f = Instantiate (floorTile, new Vector2 (x - (width / 2), y - (height / 2)), Quaternion.identity) as GameObject;
				f.transform.SetParent (floorContainer);
			}
		}
		CreateWall ();
	}
	
	private void CreateWall ()
	{
		//Upper & Lower border
		GameObject w;
		for (int x = -1; x < width+1; x++) {
			w = Instantiate (wallTile, new Vector2 (x - (width / 2), -(height / 2) - 1), Quaternion.identity) as GameObject;
			w.transform.SetParent (wallContainer);
			w = Instantiate (wallTile, new Vector2 (x - (width / 2), (height / 2) + 1), Quaternion.identity) as GameObject;
			w.transform.SetParent (wallContainer);
		}
		for (int y = 0; y < height; y++) {
			w = Instantiate (wallTile, new Vector2 ((width / 2) + 1, y - (height / 2)), Quaternion.identity) as GameObject;
			w.transform.SetParent (wallContainer);
			w = Instantiate (wallTile, new Vector2 (-(width / 2) - 1, y - (height / 2)), Quaternion.identity) as GameObject;
			w.transform.SetParent (wallContainer);
		}
	}

	private void SpawnFarmer ()
	{
		Instantiate (Farmer, farmerSpawnPosition, Quaternion.identity);
	}

	private void SpawnBull ()
	{
		Instantiate (Bull, bullSpawnPosition, Quaternion.identity);
	}

	private void CreateSpawningPoints ()
	{
		List<Vector2> usedPositions = new List<Vector2> ();

		for (int i = 0; i < numberOfSpawningPoints; i++) {

			Vector2 sPosition = new Vector2 ((int)Random.Range (-width / 2, width / 2), (int)Random.Range (-height / 2, height / 2));

			while (usedPositions.Contains (sPosition)) {
				sPosition = new Vector2 ((int)Random.Range (-width / 2, width / 2), (int)Random.Range (-height / 2, height / 2));
			}
			usedPositions.Add (sPosition);
		}

		foreach (Vector2 p in usedPositions) {
			GameObject zsp = Instantiate (spawningPoint, p, Quaternion.identity) as GameObject;
			zsp.transform.SetParent (spawnPointContainer);
		}
	}

	private void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		isGameOver = true;
		StartCoroutine (GoToGameOverScene ());
	}

	public IEnumerator GoToGameOverScene ()
	{
		yield return new WaitForSeconds (2);
		Application.LoadLevel ("GameOver");
	}
}
