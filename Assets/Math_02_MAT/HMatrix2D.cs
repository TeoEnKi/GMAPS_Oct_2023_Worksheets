using UnityEngine;

public class HMatrix2D : MonoBehaviour
{

    public float[,] Entries { get; set; } = new float[3, 3];

    //3 constructors for HMatrix2D
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

        //diagonal elements are 1
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Entries[y, x] = x == y ? 1 : 0;
            }
        }
    }

    //print matrices
    public void Print()
    {
        string grid = "";
        //for every row, every column in that row will be printed
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                grid = grid + Entries[y, x] + " ";
            }
            //after whole row is printed a new line will be appended for the second row to be printed
            grid += "\n";

        }
        Debug.Log(grid);

    }
    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right)
    {
        //create a result matrix, 
        //and for every result element, add the elements (with the same row and column) of the right and left matrix.
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
        //create a result matrix, 
        //and for every result element, subtract the element (with the same row and column) of the left matrix from the element of the right matrix
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
        //create a result matrix, 
        //and for every result element, multiply the element (with the same row and column) of the left matrix to the scalar

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
        //and for every result element, multiply the element (of a certain position in the row) of the left matrix to the element (to a same position in the column) of the Vector matrix
        return new HVector2D
        (
            left.Entries[0, 0] * right.x + left.Entries[0, 1] * right.y + left.Entries[0, 2] * right.h,
            left.Entries[1, 0] * right.x + left.Entries[1, 1] * right.y + left.Entries[1, 2] * right.h
        );
    }
    //refer to image: https://www.linkedin.com/pulse/matrix-multiplication-explained-tivadar-danka/ 
    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right)
    {
        //getlength gets the number of rows/columns in a matrix
        int leftR = left.Entries.GetLength(0);
        int rightC = right.Entries.GetLength(1);
        //use the left matrix row and right matrix column to get the dimensions of the resultant matrix
        float[,] result = new float[leftR, rightC];
        //looping through each row of 1st mat
        for (int y = 0; y < leftR; y++)
        {
            //looping through each column of 2nd mat
            for (int x = 0; x < rightC; x++)
            {
                //pos is to get the value from multiplying elements fromm a certain row/column that have the same position in that set of elements then adding those multiplied values to get the resultant value
                for (int pos = 0; pos < 3; pos++)
                {
                    result[y, x] += left.Entries[y, pos] * right.Entries[pos, x];
                }
            }
        }
        return new HMatrix2D(result);
    }
    public static bool operator ==(HMatrix2D left, HMatrix2D right)
    {
        //check if for every element in left matrix is the same for every element in the right matrix
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
        //check if any of the elements in left matrix is different from any of the element in the right matrix,
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
    //make a translation matrix based on the given translation values to move point by
    public void SetTranslationMat(float transX, float transY)
    {
        SetIdentity();
        Entries[0, 2] = transX;
        Entries[1, 2] = transY;
    }
    //make a rotation matrix based on the given degree to rotate point by (but converted to rad because of the method's parameter)
    public void SetRotationMat(float rotDeg)
    {
        SetIdentity();
        float rot = rotDeg * Mathf.Deg2Rad;
        Entries[0, 0] = Mathf.Cos(rot);
        Entries[0, 1] = -Mathf.Sin(rot);
        Entries[1, 0] = Mathf.Sin(rot);
        Entries[1, 1] = Mathf.Cos(rot);
    }

    //did not use, not in doc
    public void setScalingMat(float scaleX, float scaleY)
    {

    }
}
