using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	Node[,] grid;
	public LayerMask unwalkableMask;
	public Vector2 gridSize;
	public float nodeRadius;

	float nodeDiameter;
	int gridSizeX, gridSizeY;
	Vector3 worldBottomLeft;

	void Start() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridSize.y / nodeDiameter);
		CreateGrid (); 
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];
		worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2.0f - Vector3.forward * gridSize.y / 2.0f;

		//debug
		Debug.Log (Vector3.right * gridSize.x / 2.0f);
		Debug.Log (Vector3.up * gridSize.y / 2.0f);
		Debug.Log (transform.position);
		Debug.Log ("here");
		Debug.Log (worldBottomLeft);

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPos = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				//check whether there is collision with mask
				bool walkable = !(Physics.CheckSphere(worldPos, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPos);
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, 1, gridSizeY));
		//Gizmos.DrawCube(worldBottomLeft, Vector3.one * (nodeDiameter-.1f));

		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));

			}
		}
	}
}
