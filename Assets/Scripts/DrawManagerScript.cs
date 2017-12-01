using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawManagerScript : MonoBehaviour {

    public GameObject vertex;
    public GameObject line;

    public int multiplier = 10;

    private int state = 0;
    private int counter = 0;
    private Material m;
    private GameObject start, end;
    private float[,] graph;
    private List<Vector3> verticesPositions = new List<Vector3>();
    private int vertexCount = 0;

    // Use this for initialization
    void Start () {
        
	}

    T[,] ResizeArray<T>(T[,] original, int rows, int cols)
    {
        var newArray = new T[rows, cols];
        int minRows = Mathf.Min(rows, original.GetLength(0));
        int minCols = Mathf.Min(cols, original.GetLength(1));
        for (int i = 0; i < minRows; i++)
            for (int j = 0; j < minCols; j++)
                newArray[i, j] = original[i, j];
        return newArray;
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
    // Update is called once per frame
    void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "plane" && state == 0)
                {
                    GameObject instance = Instantiate(vertex, hit.point + new Vector3(0,0,-0.00001f), Quaternion.identity);
                    VertexScript script = instance.GetComponent<VertexScript>();
                    if (CollidesWithOthers(instance))
                        Destroy(instance);
                    else
                    {
                        script.setId(vertexCount);
                        vertexCount++;

                        if(graph == null)
                        {
                            graph = new float[1, 1];
                        }else
                        {
                            graph = ResizeArray(graph,vertexCount, vertexCount);

                        }
                        verticesPositions.Add(instance.transform.position);

                    }
                }
                else if (hit.collider.gameObject.tag == "vertex")
                {
                    
                    state = 1;
                    counter++;
                    if(counter == 1)
                    {
                        start = hit.collider.gameObject;
                        m = hit.collider.gameObject.GetComponent<Renderer>().material;
                        m.color = Color.green;
                    }
                    if (counter == 2)
                    {
                        if (!hit.collider.gameObject.transform.position.Equals(start.transform.position))
                        {
                            end = hit.collider.gameObject;

                            GameObject instance = (Instantiate(line) as GameObject).GetComponent<LineScript>().setPoints(start.transform.position, end.transform.position);
                            int id1 = start.GetComponent<VertexScript>().getId();
                            int id2 = end.GetComponent<VertexScript>().getId();
                            graph[id1, id2] = Vector3.Distance(start.transform.position * multiplier, end.transform.position * multiplier);
                            graph[id2, id1] = graph[id1, id2];


                            m.color = Color.black;
                            state = 0;
                            counter = 0;

                        }else
                        {
                            counter = 0;
                            state = 0;
                            m.color = Color.black;
                        }
                    }
                    
                }else if (hit.collider.gameObject.tag == "button" && state == 0)
                {
                    LevelManager.Load(1, graph, verticesPositions);
                }

            }
        }
    }

    bool CollidesWithOthers(GameObject obj)
    {
        foreach(Vector3 o in verticesPositions)
        {
            if(Vector3.Distance(o,obj.transform.position) < 1 )
            {
                return true;
            }
        }
        return false;
    }
}
