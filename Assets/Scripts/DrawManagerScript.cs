using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManagerScript : MonoBehaviour {

    public GameObject vertex;
    public GameObject line;

    private int state = 0;
    private int counter = 0;
    private Material m;
    private Vector3 startPoint, endPoint;
    private ArrayList vertices = new ArrayList();
    private ArrayList lines = new ArrayList();

    // Use this for initialization
    void Start () {
        
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
                    if (CollidesWithOthers(instance))
                        Destroy(instance);
                    else
                        vertices.Add(instance);
                }
                else if (hit.collider.gameObject.tag == "vertex")
                {
                    state = 1;
                    counter++;
                    if(counter == 1)
                    {
                        startPoint = hit.collider.gameObject.transform.position;
                        m = hit.collider.gameObject.GetComponent<Renderer>().material;
                        m.color = Color.green;
                    }
                    if (counter == 2)
                    {
                        if (!hit.collider.gameObject.transform.position.Equals(startPoint))
                        {
                            endPoint = hit.collider.gameObject.transform.position;

                            GameObject instance = (Instantiate(line) as GameObject).GetComponent<LineScript>().setPoints(startPoint, endPoint);
                            lines.Add(instance);

                            counter--;
                            m.color = Color.black;
                            state = 0;
                            counter = 0;

                  
                        }
                    }
                    
                }

            }
        }
    }

    bool CollidesWithOthers(GameObject obj)
    {
        foreach(GameObject o in vertices)
        {
            if(Vector3.Distance(o.transform.position,obj.transform.position) < 1 )
            {
                return true;
            }
        }
        return false;
    }
}
