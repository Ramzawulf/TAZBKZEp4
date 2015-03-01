using UnityEngine;
using System.Collections;

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
	public Transform zombieContainer;
	public Vector2[] zombieSpawnPosition;
	public GameObject[] Zombies;
	public float spawnSpeed = 0.5f;
	private float lastZombieSpawn;

	//Controlling Variables
	public bool isPaused = false;
	public bool isGameOver = false;


	void Awake ()
	{
		PaintMyGridYall ();
		SpawnFarmer ();
		SpawnBull ();
		lastZombieSpawn = Time.time;

		if (control == null) {
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			isPaused = !isPaused;
		}

		if (isPaused || isGameOver) 
			return;

		if (Time.time >= (lastZombieSpawn + spawnSpeed)) {
			SpawnZombie ();
			lastZombieSpawn = Time.time;
		}
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

	private void SpawnZombie ()
	{
		GameObject z = Instantiate (Zombies [Random.Range (0, Zombies.Length)], 
		             zombieSpawnPosition [Random.Range (0, zombieSpawnPosition.Length)], 
		             Quaternion.identity) as GameObject;
		z.transform.SetParent (zombieContainer);

	}

	public void GameOver ()
	{
		isGameOver = true;
	}
}
