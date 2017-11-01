using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	Node[,] grid;
	public LayerMask unwalkableMask;
	public Vector2 gridSize;
	public float nodeRadius;
	public Transform player;

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

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPos = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				//check whether there is collision with mask
				bool walkable = !(Physics.CheckSphere(worldPos, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPos);
			}
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentageX = (worldPosition.x + gridSize.x/2.0f)/gridSize.x;
		float percentageY = (worldPosition.z + gridSize.y/2.0f)/gridSize.y;
		percentageX = Mathf.Clamp01 (percentageX);
		percentageY = Mathf.Clamp01 (percentageY);

		int x = Mathf.RoundToInt((gridSizeX -1) * percentageX);
		int y = Mathf.RoundToInt((gridSizeY -1) * percentageY);
		return grid[x,y];
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
		Gizmos.color = Color.blue;
		Node position = NodeFromWorldPoint (player.position);
		Gizmos.DrawCube(position.worldPosition, Vector3.one * (nodeDiameter-.1f));
	}
		
}
