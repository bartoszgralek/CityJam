﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour{

    private int id;
    private Vector3 middlePoint;
    private List<VertexVariation> outs;
    private int idGreen;

    public Crossing(int id, Vector3 middlePoint)
    {
        this.middlePoint = middlePoint;
        this.id = id;
		this.outs = new List<VertexVariation>();
    }

    public void addRoad(VertexVariation vertexVariation)
    {
        outs.Add(vertexVariation);
    }

    public int getId()
    {
        return this.id;
    }

    public Vector3 getMiddlePointForRoad(int index)
    {
        Vector3 output = new Vector3();
        for(int i=0;i<outs.Count;i++)
        {
            if(outs[i].getId() == index)
            {
            
				output.x = (outs [i].getVectIn ().x + outs [i].getVectOut ().x)/2;
				output.y = 0;
				output.z = (outs [i].getVectIn ().z + outs [i].getVectOut ().z)/2;
                break;
            }
        }
		Debug.Log (output);
        return output;
    }

    public Vector3[] getPathForCrossing(int idFrom, int idTo)
    {
        Vector3 from = new Vector3();
        Vector3 to = new Vector3();
        int n = 0;
        for(int i=0;i<outs.Count;i++)
        {
            if(outs[i].getId() == idFrom)
            {
                from = outs[i].getVectIn();
                n++;
            }

            if(outs[i].getId() == idTo)
            {
                to = outs[i].getVectOut();
                n++;
            }
        }

        if(n!=2)
        {
            throw new NoNeighbourException("no such connections");
        }

        return BezierCurve.getBezierPoints(from, middlePoint, to);
    }

    private void Start()
    {
        idGreen = outs[0].getId();
       
    }

    private void Update()
    {
        turnLights();
    }

    void turnLights()
    {
        
    }
}
