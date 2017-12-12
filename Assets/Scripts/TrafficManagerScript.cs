using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrafficManagerScript : MonoBehaviour {

    public GameObject car;
    public GameObject Vertex;
    private Graph g;
    private float[,] graph;
    private List<Vector3> verticesPositions;

    private void Awake()
    {
        //fill graph with vertices to execute Dijstra Algorithm
        g = new Graph();
        graph = LevelManager.getGraph();
        verticesPositions = LevelManager.getVerticesPositions();
        Dictionary<int, float> dir = new Dictionary<int, float>();
        for (int i=0;i< graph.GetLength(0); i++)
        {
            for(int j=0;j<graph.GetLength(1); j++)
            {
                if (graph[i,j] > 0)
                {
                    dir.Add(j, graph[i, j]);
                }
            }
            g.add_vertex(i, dir);
            dir = new Dictionary<int, float>();
        }
    }

    // Use this for initialization
    void Start () {
		carFinished();
		carFinished();
		carFinished();
		carFinished();
		carFinished();
	}

    public void carFinished()
    {
		List<int> path;
		int startId;
		int endId;

			startId = UnityEngine.Random.Range(0, verticesPositions.Count);
			do {
			endId = UnityEngine.Random.Range(0, verticesPositions.Count);
			} while (startId.Equals(endId));

			path = g.shortest_path (startId, endId);
		

        if (path == null)
        {
            Debug.Log("Path is null?");
        }
        else
        {
            //Debug.Log("Points: " + startId + " -> " + endId);
            //Debug.Log("Path: ");
            path.Add(startId);
            path.Reverse();
        }

        List<Vector3> pathForCar = new List<Vector3>();
		Vector3[] help;
		//pathForCar.Add(verticesPositions[path[0]]);
        for (int i = 1; i < path.Count-1; i++)
        {
            //pathForCar.Add(verticesPositions[path[i]]);
			help = MapManagerScript.getCrossingAtIndex(path[i]).getPathForCrossing(path[i-1], path[i+1]);
			foreach (Vector3 v in help)
			{
				pathForCar.Add(v);
			}
			pathForCar.RemoveAt (pathForCar.Count - 1);
        }
		if (pathForCar.Count > 5) {
			for (int h = 0; h < 5; h++) {
				pathForCar.RemoveAt (pathForCar.Count - 1);
			}
			/*for (int j = 0; j < pathForCar.Count; j++)
			{
				Instantiate(Vertex, pathForCar[j], Quaternion.identity);
			}*/
			//Debug.Log (pathForCar.Count);
			GameObject instance = (Instantiate(car, pathForCar[0], Quaternion.identity) as GameObject).GetComponent<CarScript>().SetUp(pathForCar,this);

		} else {
			carFinished ();
		}

        
    }
    
    // Update is called once per frame
    void Update() {

    }
}
