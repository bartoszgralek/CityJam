using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapManagerScript : MonoBehaviour {

    public GameObject prefab;
    private List<Vector3> verticesPositions;
    private float[,] graph;

    private void Awake()
    {
        graph = LevelManager.getGraph();
        verticesPositions = LevelManager.getVerticesPositions();
        if(graph != null && verticesPositions != null)
        {
            print("Hurey!");
        }
    }
    // Use this for initialization
    void Start () {
        print("Positions: \n\n\n");
        foreach(Vector3 v in verticesPositions)
        {
            print(v);
        }

        print("graph size: " + graph.GetLength(0) + " " + graph.GetLength(1));
        for (int i = 0; i < graph.GetLength(0); i++)
        {
            for (int j = i; j < graph.GetLength(1); j++)
            {
                print("for " + i + ":" + j + " " + graph[i, j]);
                if(graph[i,j]>0)
                {
                    Road r = Instantiate(prefab).GetComponent<Road>();
                    r.points.Clear();
                    Vector3 start = verticesPositions[i] * 10;
                    start.z = start.y;
                    start.y = 0;

                    Vector3 end = verticesPositions[j] * 10;
                    end.z = end.y;
                    end.y = 0;

                    r.points.Add(start);
                    r.points.Add(end);
                    r.Refresh();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
