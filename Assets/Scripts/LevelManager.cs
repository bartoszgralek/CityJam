using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager {

    private static float[,] graph;
    private static List<Vector3> verticesPositions;

    public static void Load(int id, float[,] graph, List<Vector3> verticesPositions)
    {
        LevelManager.graph = graph;
        LevelManager.verticesPositions = verticesPositions;

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
