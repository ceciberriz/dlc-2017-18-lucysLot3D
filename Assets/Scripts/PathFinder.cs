using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	Grid grid;

	void Awake() {
		grid = GetComponent<Grid> ();
	}

	void FindPath (Vector3 startPosition, Vector3 targetPosition) {
		Node startNode = grid.NodeFromWorldPoint (startPosition);
		Node targetNode = grid.NodeFromWorldPoint (targetPosition);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		openSet.Add (startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet[0];
			for (int i = 1; openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost || (openSet [i].fCost == currentNode.fCost && openSet [i].hCost < currentNode.hCost)) {
					currentNode = openSet [i];
				}
			}

			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				return;
			}

			foreach (Node neighbour in grid.getNodeNeighbors(currentNode) {
				//loop through neighbors and check whether the neighbor is not wakable 
				// or is in closed list.
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}
			}
		}
	}
}
