using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground 
{
    private Transform G;
    public Ground(Transform g)
    {
        this.G = g;
    }
    public void Move(float y)
    {
        G.position += new Vector3(-1, 0, 0) * y * Time.deltaTime;
    }

    public float X()
    {
        return G.position.x;
    }
    public float Y()
    {
        return G.position.y;
    }

    public Transform GetTransform()
    {
        return G;
    }
}
