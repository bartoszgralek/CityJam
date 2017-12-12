using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class MapManagerScript : MonoBehaviour {

    public GameObject prefab;
    public GameObject ball;
    
    private List<Vector3> verticesPositions;
    private float[,] graph;
    private static List<Crossing> crossings;
    //private int multiplier = 10;

    private void Awake()
    {
        graph = LevelManager.getGraph();
        verticesPositions = LevelManager.getVerticesPositions();
        crossings = new List<Crossing>();
        if(graph != null && verticesPositions != null)
        {
            
        }
    }

    public static void Print2DArray<T>(T[,] matrix)
    {
        /*for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Debug.Log(matrix[i, j] + " ");
            }
            Debug.Log("\n");
        }*/
    }
    // Use this for initialization
    void Start () {
        if (graph != null)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                Crossing crossing = new Crossing(i, verticesPositions[i]);
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                    {
                        
                        Vector3 from = verticesPositions[i];
                        Vector3 to = verticesPositions[j];
                        Vector3 vectIn = new Vector3();
                        Vector3 vectOut = new Vector3();

                        //first point c(x,y)
                        float a1 = (to.z - from.z) / (to.x - from.x);
                        float b1 = from.z - a1 * from.x;
                        float d1 = 2f;
                        float d2 = 1f;
                        float x;
                        if((to.x - from.x)>0)
                        {
                            x = from.x + Mathf.Sqrt(Mathf.Pow(d1, 2f) / (Mathf.Pow(a1, 2f) + 1));
                        }
                        else
                        {
                            x = from.x - Mathf.Sqrt(Mathf.Pow(d1, 2f) / (Mathf.Pow(a1, 2f) + 1));
                        }

                        float y = a1 * x + b1;

                        //now two points ( directions ) 
                        float a2 = -1 / a1;
                        float b2 = y - a2 * x;
                        float x1 = x + Mathf.Sqrt(Mathf.Pow(d2, 2f) / (Mathf.Pow(a2, 2f) + 1));
                        float x2 = x - Mathf.Sqrt(Mathf.Pow(d2, 2f) / (Mathf.Pow(a2, 2f) + 1));

                        float y1 = a2 * x1 + b2;
                        float y2 = a2 * x2 + b2;


						if (from.z < a2 * from.x + b2) {
							vectOut = new Vector3 (x1, 0, y1);
							vectIn = new Vector3(x2, 0, y2);
						} else {
							vectOut = new Vector3(x2, 0, y2);
							vectIn = new Vector3(x1, 0, y1);
						}


                        /*float diff = y - from.y;
                        if(diff>0)
                        {
                            vectIn = new Vector3(x2, 0, y2);
                            vectOut = new Vector3(x1, 0, y1);
                        }else
                        {
                            vectOut = new Vector3(x2, 0, y2);
                            vectIn = new Vector3(x1, 0, y1);
                        //}*/

                        //Instantiate(ball, vectIn, Quaternion.identity);
                        //Instantiate(ball, vectOut, Quaternion.identity);
                        crossing.addRoad(new VertexVariation(j, vectIn, vectOut));

                       

                        //r.Refresh();
                        //r.gameObject.AddComponent<BoxCollider>();
					}
				}
				crossings.Add(crossing);
            }

            for(int i=0; i<graph.GetLength(0); i++)
            {
                for(int j=0; j<i; j++)
                {
                    if(graph[i,j] > 0)
                    {
                        Road r = Instantiate(prefab).GetComponent<Road>();
                        r.points.Clear();

                        Vector3 start = new Vector3();
                        start = crossings[i].getMiddlePointForRoad(j);
                        //Debug.Log(start);

                        Vector3 end = new Vector3();
                        end = crossings[j].getMiddlePointForRoad(i);
                        //Debug.Log(end);

						Debug.Log ("///" + start + "///" + end);
                        r.points.Add(start);
                        r.points.Add(end);

                        r.Refresh();
                        r.gameObject.AddComponent<BoxCollider>();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

	public static Crossing getCrossingAtIndex(int index)
	{
		return crossings[index];
	}

}
