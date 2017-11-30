using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapManagerScript : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "plane")
                {
                    Road r = Instantiate(prefab).GetComponent<Road>();
                    r.points.Clear();
                    r.points.Add(new Vector3(Mathf.Floor(Random.Range(-10,10)), Mathf.Floor(Random.Range(-10, 10)), Mathf.Floor(Random.Range(-10, 10))));
                    r.points.Add(new Vector3(Mathf.Floor(Random.Range(-10, 10)), Mathf.Floor(Random.Range(-10, 10)), Mathf.Floor(Random.Range(-10, 10))));
                    r.Refresh();
                }
            }

        }
    }
}
