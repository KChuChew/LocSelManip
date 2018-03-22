using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour {

	public Transform prefab;
    RaycastHit hit;
	int numPoints = 24;
	Vector3 centerPos = new Vector3(0, (float)0.5, 0);

	double initAngle = 90;
	int maxHeight = 15;

	void Start() {
		/* loop generating even layers of brick wall*/
		// outer loop generates maxHeight/2 layers (odd layers)
		for (int height = 0; height < maxHeight; height += 2) {
			// inner loop generates 24 bricks for each layer of the wall
			for (int pointNum = 0; pointNum < numPoints; pointNum++) {

				// pointNum = current brick we're generating
				float i = (float)(pointNum * 1.0 / numPoints);
				float angle = i * Mathf.PI * 2;
				// standard coordinate calculation for points around a circle (brick wall radius = 7.5m)
				float x = (float)(Mathf.Cos (angle) * 7.5);
				float y = (float)(Mathf.Sin (angle) * 7.5); 
				// calc height and rotation of each brick
				Vector3 pos = new Vector3 (x, height, y) + centerPos;
				Vector3 rot = new Vector3 (0, (float)initAngle, 0);
				Instantiate (prefab, pos, Quaternion.Euler (rot));
				// each brick placed should rotate -15 degrees from previous for circular effect
				initAngle -= 15;
			}
			// reset brick rotation for new layer of wall
			initAngle = 90;
		}

		// new rotation angle for brick offset aesthetic
		initAngle = 82.5;
		/* loop generating even layers of brick wall (rotation offset for aesthetics) */
		// outer loop generates maxHeight/2 layers (even layers)
		for (int height = 1; height < maxHeight; height += 2) {
			// inner loop generates 24 bricks for each layer of the wall
			for (int pointNum = 0; pointNum < numPoints; pointNum++) {

				// pointNum = current brick we're generating (+0.5 for brick aesthetic offset)
				float i = (float)((pointNum + 0.5) * 1.0 / numPoints);
				// standard coordinate calculation for points around circle (brick wall radius = 7.5m)
				float angle = (float)(i * Mathf.PI * 2);
				float x = (float)(Mathf.Cos (angle) * 7.5);
				float y = (float)(Mathf.Sin (angle) * 7.5); 
				// calc height and rotation of each brick
				Vector3 pos = new Vector3 (x, height, y) + centerPos;
				Vector3 rot = new Vector3 (0, (float)initAngle, 0);
				Instantiate (prefab, pos, Quaternion.Euler (rot));
				//Instantiate (prefab, pos, Quaternion.identity);
				// each brick placed should rotate -15 degrees from previous for circular effect
				initAngle -= 15;
			}
			// reset brick rotation for new layer of wall
			initAngle = 82.5;
		}
	}

	// Update is called once per frame
	/*void Update () {

    }*/
}
