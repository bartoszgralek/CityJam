using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexVariation {

    private int id;
    private Vector3 vectIn, vectOut;

    public VertexVariation(int id, Vector3 vectIn, Vector3 vectOut)
    {
        this.id = id;
        this.vectIn = vectIn;
        this.vectOut = vectOut;
    }

    public int getId()
    {
        return id;
    }

    public Vector3 getVectIn()
    {
        return vectIn;
    }

    public Vector3 getVectOut()
    {
        return vectOut;
    }

    public void setVectIn(Vector3 vector)
    {
        this.vectIn = vector;
    }

    public void setVectOut(Vector3 vector)
    {
        this.vectOut = vector;
    }
}