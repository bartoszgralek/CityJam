using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    public float speed;

    private List<Vector3> path;
    private int current = 0;

    public GameObject SetPath(List<Vector3> path)
    {
        this.path = path;
        return this.gameObject;
    }

	// Use this for initialization
	void Start () {
        transform.position = path[0] + new Vector3(0, 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.Equals(path[path.Count - 1]))
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (transform.position != path[current])
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, path[current], speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }
            else
            {
                current++;
            }
        }
	}
}
