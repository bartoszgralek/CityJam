using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    public float speed = 5;

    private TrafficManagerScript trafficManager;
    private List<Vector3> path;
    public Vector3[] tmp;
    private int current = 0;
    private bool keepGoing = true;
    private Vector3 target = new Vector3();

    public GameObject SetUp(List<Vector3> path, TrafficManagerScript trafficManager)
    {
        
        tmp = new Vector3[path.Count];
        this.trafficManager = trafficManager;
        path.CopyTo(tmp);
        /*for (int i = 0; i < tmp.Length; i++)
            Debug.Log("PATH for i=" + i + ":" + tmp[i]);*/
        
        return this.gameObject;
    }

    private void Awake()
    {

    }



    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        keepGoing = true;
        RaycastHit hit;
        float theDistance;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 4;
        
        
        Debug.DrawRay(transform.position+ transform.TransformDirection(Vector3.forward) + new Vector3(0, 0.5f, 0), forward, Color.green);

        if(Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) + new Vector3(0, 0.5f, 0), (forward), out hit))
        {
            theDistance = hit.distance;
            Debug.Log("collider name: " + hit.collider.gameObject.name + " dist:" + theDistance);
            if(hit.collider.gameObject.name.Equals("jeep") && theDistance<2f)
            {
                keepGoing = false;
            }
        }

        // check if we have somewere to walk
        if (tmp.Length > 0)
        {
            if (current < tmp.Length && keepGoing)
            {
                target = tmp[current];
                walk();
            }
            else if (current == tmp.Length)
            {
                if (trafficManager != null)
                    trafficManager.carFinished();
                Destroy(this.gameObject);
            }
        }
        
    }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, target - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            current++;
        }
    }

}
