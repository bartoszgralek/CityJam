using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{

    private static float[,] graph;
    private static List<Vector3> verticesPositions;
    private static int multiplier = 10;

    public static void Load(int id, float[,] graph, List<Vector3> verticesPositions)
    {
        LevelManager.graph = graph;
        LevelManager.verticesPositions = verticesPositions;
        for (int i = 0; i < LevelManager.verticesPositions.Count; i++)
        {
            LevelManager.verticesPositions[i] = new Vector3(LevelManager.verticesPositions[i].x, 0, LevelManager.verticesPositions[i].y);
            LevelManager.verticesPositions[i] *= multiplier;
        }
        SceneManager.LoadScene(id);
    }

    public static float[,] getGraph()
    {
        return graph;
    }

    public static List<Vector3> getVerticesPositions()
    {
        return verticesPositions;
    }


}