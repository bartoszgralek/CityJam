using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour {

    public Color c1 = Color.black;
    private Vector3 startPoint, endPoint;

    public GameObject setPoints(Vector3 startPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        return gameObject;
    }

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = Resources.Load("Materials/LineMat", typeof(Material)) as Material;
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c1;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

    }

    void Update()
    {
        
    }
}
