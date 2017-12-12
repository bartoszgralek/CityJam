using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    public float speed = 20;

    private TrafficManagerScript trafficManager;
    private List<Vector3> path;
    private Vector3[] tmp;
    private int current = 0;
    private Vector3 target = new Vector3();

    public GameObject SetUp(List<Vector3> path, TrafficManagerScript trafficManager)
    {
        
        tmp = new Vector3[path.Count];
        this.trafficManager = trafficManager;
        path.CopyTo(tmp);
        for (int i = 0; i < tmp.Length; i++)
            Debug.Log("PATH for i=" + i + ":" + tmp[i]);
        
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
       // check if we have somewere to walk
        if (current < tmp.Length)
        {
            target = tmp[current];
            walk();
        }else if(current == tmp.Length)
        {
			trafficManager.carFinished();
            Destroy(this.gameObject);
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
