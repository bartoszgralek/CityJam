using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoNeighbourException : System.Exception
{
    public NoNeighbourException() : base() { }
    public NoNeighbourException(string message) : base(message) { }
    public NoNeighbourException(string message, System.Exception inner) : base(message, inner) { }
}