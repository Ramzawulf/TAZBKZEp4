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
						for (int x = 0; x < width; x++) {
									for (int y = 0; y < height; y++) {
												GameObject tile = Instantiate (floorTile, new Vector2 (x - (width / 2), y - (height / 2)), Quaternion.identity) as GameObject;
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
	
}
