using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DijkstraAlgorithm {

    private static int MinimumDistance(float[] distance, bool[] shortestPathTreeSet, int verticesCount)
    {
        float min = float.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private static void Print(float[] distance, int verticesCount)
    {
        /*Debug.Log("Vertex    Distance from source");

        for (int i = 0; i < verticesCount; ++i)
            Debug.Log(i + "\t" + distance[i]);*/
    }

    public static List<KeyValuePair<int,float>> Dijkstra(float[,] graph, int source, int verticesCount)
    {
        List<KeyValuePair<int, float>> output = new List<KeyValuePair<int, float>>();
        float[] distance = new float[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = float.MaxValue;
            shortestPathTreeSet[i] = false;
        }

        distance[source] = 0;

        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    distance[v] = distance[u] + graph[u, v];
        }

        /*for (int i = 0; i < verticesCount; ++i)
            Debug.Log(shortestPathTreeSet[i]);*/

        return output;
    }
}
