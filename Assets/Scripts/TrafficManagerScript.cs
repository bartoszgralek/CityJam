using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrafficManagerScript : MonoBehaviour {

    public GameObject car;
    private Graph g;
    private float[,] graph;
    private List<Vector3> verticesPositions;

    private void Awake()
    {
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
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {

            int startId = UnityEngine.Random.Range(0, verticesPositions.Count);
            int endId = UnityEngine.Random.Range(0, verticesPositions.Count);
            while (startId.Equals(endId))
            {
                endId = UnityEngine.Random.Range(0, verticesPositions.Count);
            }

            List<int> path = g.shortest_path(startId, endId);

            if (path == null)
            {
                Debug.Log("Path is null?");
            }else
            {
                Debug.Log("Points: " + startId + " -> " + endId);
                Debug.Log("Path: ");
                path.Add(startId);
                path.Reverse();
                for (int i =0;i<path.Count;i++)
                {
                    Debug.Log(path[i]);
                }
            }

            List<Vector3> pathForCar = new List<Vector3>();
            for(int i=0;i<path.Count;i++)
            {
                pathForCar.Add(verticesPositions[path[i]]);
                Debug.Log("Vector3 for car: " + pathForCar[i]);
            }

            GameObject instance = (Instantiate(car) as GameObject).GetComponent<CarScript>().SetPath(pathForCar);

        }
    }
}
