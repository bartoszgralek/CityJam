using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class MapManagerScript : MonoBehaviour {

    public GameObject prefab;
    
    private List<Vector3> verticesPositions;
    private float[,] graph;
    private int multiplier = 10;

    private void Awake()
    {
        graph = LevelManager.getGraph();
        verticesPositions = LevelManager.getVerticesPositions();
        if(graph != null && verticesPositions != null)
        {
            print("Hurey!");
            
        }
    }

    public static void Print2DArray<T>(T[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Debug.Log(matrix[i, j] + " ");
            }
            Debug.Log("\n");
        }
    }
    // Use this for initialization
    void Start () {
        if (graph != null)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = i; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                    {
                        Road r = Instantiate(prefab).GetComponent<Road>();
                        r.points.Clear();

                        

                        Vector3 start = verticesPositions[i];
                        Vector3 end = verticesPositions[j];

                        r.points.Add(start);
                        r.points.Add(end);
                        
                        r.Refresh();
                        //r.gameObject.AddComponent<BoxCollider>();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
