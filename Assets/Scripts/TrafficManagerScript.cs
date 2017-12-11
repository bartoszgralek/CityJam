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
	}

    public void carFinished()
    {
        int startId = UnityEngine.Random.Range(0, verticesPositions.Count-1);
        int endId = UnityEngine.Random.Range(0, verticesPositions.Count-1);
        while (startId.Equals(endId))
        {
            endId = UnityEngine.Random.Range(0, verticesPositions.Count-1);
        }

        List<int> path = g.shortest_path(startId, endId);

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
        for (int i = 0; i < path.Count; i++)
        {
            pathForCar.Add(verticesPositions[path[i]]);
        }

        for (int j = 0; j < pathForCar.Count; j++)
        {
            Instantiate(Vertex, pathForCar[j], Quaternion.identity);
        }

        GameObject instance = (Instantiate(car, pathForCar[0], Quaternion.identity) as GameObject).GetComponent<CarScript>().SetUp(pathForCar,this);

    }
    
    // Update is called once per frame
    void Update() {

    }
}
