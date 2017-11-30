using System;
using UnityEditor;
using UnityEngine;

public class BezierCurve
	{

	public static Vector3[] getBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2) {
		Vector3[] toReturn = new Vector3[11];
		toReturn[0] = p0;
		for (int i = 1; i < 10; i++) {
			toReturn[i] = calculateBezierPoint (i * 0.1f, p0, p1, p2);
		}
		toReturn [10] = p2;
		return toReturn;
	}
	
	private static Vector3 calculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
		{ //[x,y]=(1–t)2P0+2(1–t)tP1+t2P2
		
		Vector3 p = (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;

		return p;
		}

	}


