using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Unity.VisualScripting;

//[Serializable]
public class HVector2D
{
    public float x, y;
    public float h;
    //3 HVector2D constructors
    public HVector2D(float _x, float _y)
    {
        x = _x;
        y = _y;
        h = 1.0f;
    }

    public HVector2D(Vector2 _vec)
    {
        x = _vec.x;
        y = _vec.y;
        h = 1.0f;
    }

    public HVector2D()
    {
        x = 0;
        y = 0;
        h = 1.0f;
    }
    //overloading operator
    public static HVector2D operator +( HVector2D a, HVector2D b)
    {
        return new HVector2D(a.x + b.x, a.y + b.y);
    }

    public static HVector2D operator -( HVector2D a, HVector2D b)
    {
        return new HVector2D(a.x - b.x, a.y - b.y);
    }

    public static HVector2D operator *(HVector2D a, float scalar)
    {
        return new HVector2D(a.x * scalar, a.y * scalar);
    }

    public static HVector2D operator /(HVector2D a, float scalar)
    {
        return new HVector2D(a.x / scalar, a.y / scalar);
    }

    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y );
    }

    public void Normalize()
    {
        float mag = Magnitude();
        x /= mag;
        y /= mag;
    }

    public float DotProduct(HVector2D vec)
    {
        //length of projection of vec on another vector
        return (x * vec.x + y * vec.y);
    }

    public HVector2D Projection(HVector2D b)
    {
        //vector of projection from orgin proj a/ proj b
        HVector2D proj = b * (DotProduct(b) / b.DotProduct(b));
        return proj;

    }

    public float FindAngle(HVector2D vec)
    {
        //returns the cosine angle that is between the 2 vectors
        return (float)Mathf.Acos(DotProduct(vec) / (Magnitude() * vec.Magnitude()));
    }

    public Vector2 ToUnityVector2()
    {
        return new Vector2(x, y); 
    }

    public Vector3 ToUnityVector3()
    {
        return new Vector3(x,y,0); 
    }

    // public void Print()
    // {

    // }
}
