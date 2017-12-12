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
							Vector3 v1, v2, v3, v4, k;
							float a, b, c, d, kx, ky;
							bool isInside = false;
							for (int i = 0; i < graph.GetLength (0); i++) {
								for (int j = 0; j < i; j++) {
									Debug.Log (j + "---" + i);
									if (graph[i, j] != 0) {
										v1 = start.transform.position;
										v2 = verticesPositions [i];
										v3 = end.transform.position;
										v4 = verticesPositions [j];

										if (v1 == v2 || v1 == v4 || v3 == v2 || v3 == v4) {
											Debug.Log ("wspólne wierzchołki");
											continue;
										}

										Debug.Log (v1 + " " + v2 + " " + v3 + " " + v4);
										a = (v1.y - v3.y) / (v1.x - v3.x);
										b = v1.y - a * v1.x;
										c = (v2.y - v4.y) / (v2.x - v4.x);
										d = v2.y - c * v2.x;
										Debug.Log (a + " " + b + " " + c + " " + d);


										if (c != a) {
											kx = (b - d) / (c - a);
											ky = a * kx + b;
											Debug.Log (kx + " " + ky);
												k.x = kx;
												k.y = ky;
												k.z = 0;
											//GameObject instance = Instantiate(vertex, k, Quaternion.identity);
										} else {
											Debug.Log ("równoległe");
											continue;
										}

										isInside = false;
										isInside = checkIfInside (v1, v2, k, isInside);
										isInside = checkIfInside (v2, v3, k, isInside);
										isInside = checkIfInside (v3, v4, k, isInside);
										isInside = checkIfInside (v4, v1, k, isInside);

										if (isInside) {
											Debug.Log ("jest zły");
											goto waypoint;
										}
									}
								}
							}
							waypoint:

							if (!isInside) {
								GameObject instance = (Instantiate(line) as GameObject).GetComponent<LineScript>().setPoints(start.transform.position, end.transform.position);
								int id1 = start.GetComponent<VertexScript>().getId();
								int id2 = end.GetComponent<VertexScript>().getId();
								graph[id1, id2] = Vector3.Distance(start.transform.position * multiplier, end.transform.position * multiplier);
								graph[id2, id1] = graph[id1, id2];
							}
                           

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

	bool checkIfInside(Vector3 i, Vector3 j, Vector3 k, bool isInside) {
		if (((i.y > k.y) != (j.y > k.y)) && (k.x < (j.x - i.x) * (k.y - i.y) / (j.y - i.y) + i.x))
			return !isInside;
		return isInside;
	}
}


