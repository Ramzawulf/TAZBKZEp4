using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public int width = 19;
	public int height = 19;
	public GameObject floorTile;
	public GameObject wallTile;
	
	void Awake ()
	{
		PaintMyGridYall ();
	}
	
	void Update ()
	{
		
	}
	
	public void PaintMyGridYall ()
	{
		// 9x9
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject tile = Instantiate (floorTile, new Vector2 (x - (width / 2), y - (height / 2)), Quaternion.identity) as GameObject;
			}
		}
	}
	
	private void CreateWall ()
	{
		
	}
	
}
