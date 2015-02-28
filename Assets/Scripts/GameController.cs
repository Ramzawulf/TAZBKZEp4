using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public int width = 19;
	public int height = 19;
	public GameObject floorTile;
	public GameObject wallTile;
	//Farmer
	public Vector2 farmerSpawnPosition = Vector2.zero;
	public GameObject Farmer;
	//Bull
	public Vector2 bullSpawnPosition = new Vector2 (1, 1);
	public GameObject Bull;
	//Zombie
	public Vector2[] zombieSpawnPosition;
	public GameObject[] Zombies;
	public float spawnSpeed = 0.5f;
	private float lastZombieSpawn;

	void Awake ()
	{
		PaintMyGridYall ();
		SpawnFarmer ();
		SpawnBull ();
		lastZombieSpawn = Time.time;
	}
	
	void Update ()
	{
		if (Time.time >= (lastZombieSpawn + spawnSpeed)) {
			SpawnZombie ();
			lastZombieSpawn = Time.time;
		}
	}
	
	public void PaintMyGridYall ()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				Instantiate (floorTile, new Vector2 (x - (width / 2), y - (height / 2)), Quaternion.identity);
			}
		}
		CreateWall ();
	}
	
	private void CreateWall ()
	{
		//Upper & Lower border
		for (int x = -1; x < width+1; x++) {
			Instantiate (wallTile, new Vector2 (x - (width / 2), -(height / 2) - 1), Quaternion.identity);
			Instantiate (wallTile, new Vector2 (x - (width / 2), (height / 2) + 1), Quaternion.identity);
		}
		for (int y = 0; y < height; y++) {
			Instantiate (wallTile, new Vector2 ((width / 2) + 1, y - (height / 2)), Quaternion.identity);
			Instantiate (wallTile, new Vector2 (-(width / 2) - 1, y - (height / 2)), Quaternion.identity);
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
		Instantiate (Zombies [Random.Range (0, Zombies.Length)], 
		             zombieSpawnPosition [Random.Range (0, zombieSpawnPosition.Length)], 
		             Quaternion.identity);
	}
}
