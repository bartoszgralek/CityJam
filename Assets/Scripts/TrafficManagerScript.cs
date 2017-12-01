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
            Vector3 startPosition = verticesPositions[UnityEngine.Random.Range(0, verticesPositions.Count - 1)];
            Vector3 endPosition = verticesPositions[UnityEngine.Random.Range(0, verticesPositions.Count - 1)];
            while (startPosition.Equals(endPosition))
            {
                endPosition = verticesPositions[UnityEngine.Random.Range(0, verticesPositions.Count - 1)];
            }

            
            if(g.shortest_path(0, 2) == null)
            {
                Debug.Log("Path is null?");
            }
            else
            {
                Debug.Log("Path: ");
                g.shortest_path(0, 2).ForEach(x => Debug.Log(x));
            }
        }
    }
}
