using UnityEngine;

public class HMatrix2D : MonoBehaviour
{
    public float[,] Entries { get; set; } = new float[3, 3];

    public HMatrix2D()
    {
        SetIdentity();
    }
    public HMatrix2D(float[,] multiArray)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Entries[y, x] = multiArray[y, x];
            }
        }
    }
    public HMatrix2D(float m00, float m01, float m02,
        float m10, float m11, float m12,
        float m20, float m21, float m22)
    {
        //1st row
        Entries[0, 0] = m00;
        Entries[0, 1] = m01;
        Entries[0, 2] = m02;

        //2nd row
        Entries[1, 0] = m10;
        Entries[1, 1] = m11;
        Entries[1, 2] = m12;

        //3rd row
        Entries[2, 0] = m20;
        Entries[2, 1] = m21;
        Entries[2, 2] = m22;

    }
    public void SetIdentity()
    {
        //for (int y = 0; y < 3; y++)
        //{
        //    for (int x = 0; x < 3; x++)
        //    {
        //        if (x == y)
        //        {
        //            Entries[y, x] = 1;
        //        }
        //        else
        //        {
        //            Entries[y, x] = 0;
        //        }
        //    }
        //}
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Entries[y, x] = x == y ? 1 : 0;
            }
        }
    }
    public void Print()
    {
        string grid = "";

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                grid = grid + Entries[y, x] + " ";
            }
            grid += "\n";

        }
        Debug.Log(grid);

    }
    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] + right.Entries[y, x];
            }
        }
        return result;
    }
    public static HMatrix2D operator -(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] - right.Entries[y, x];
            }
        }
        return result;
    }
    public static HMatrix2D operator *(HMatrix2D left, float scalar)
    {
        HMatrix2D result = new HMatrix2D();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] * scalar;
            }
        }
        return result;
    }
    public static HVector2D operator *(HMatrix2D left, HVector2D right)
    {
        return new HVector2D
            (
                left.Entries[0, 0] * right.x + left.Entries[0, 1] * right.y,
                left.Entries[1, 0] * right.x + left.Entries[1, 1] * right.y
            );
    }
    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int yResult = 0; yResult < 3; yResult++)
        {
            for (int xResult = 0; xResult < 3; xResult++)
            {
                for (int pos = 0; pos < 3; pos++)
                {
                    result.Entries[yResult, xResult] += left.Entries[yResult, pos] * right.Entries[pos, xResult];

                }
            }
        }
        return result;
    }

    public static bool operator ==(HMatrix2D left, HMatrix2D right)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (left.Entries[y, x] != right.Entries[y, x])
                {
                    return false;
                }
            }
        }
        return true;
    }
    public static bool operator !=(HMatrix2D left, HMatrix2D right)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (left.Entries[y, x] != right.Entries[y, x])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
