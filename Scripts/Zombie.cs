using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie
{
    private Transform G;
    public Zombie(Transform g)
    {
        this.G = g;
    }
    public void Move(float y)
    {
        G.position += new Vector3(-1, 0, 0) * y* Time.deltaTime;
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
