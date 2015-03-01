using UnityEngine;
using System.Collections;

public class SpawningPoint : MonoBehaviour
{
	public GameObject[] zombies;

	// Z/s
	public float spawnRate = 1.3f;
	public float spawnChance = 0.3f;

	private float lastSpawn;
	private float spawnInverse{ get { return 1 / spawnRate; } }

	void Start ()
	{
		lastSpawn = Time.time;
	}

	void Update ()
	{
		if (GameController.control.isPaused || GameController.control.isGameOver)
			return;

		if (CanSpawn ()) {
			SpawnZombie ();
		}
	}

	private bool CanSpawn ()
	{
		return (Random.Range (0, 1) <= spawnChance) && (Time.time >= (lastSpawn + spawnInverse));
	}

	public void SpawnZombie ()
	{
		Instantiate (zombies [Random.Range (0, zombies.Length)], transform.position, Quaternion.identity);
		lastSpawn = Time.time;
	}
}
