using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TestMatrix : MonoBehaviour
{
    [SerializeField]
    GameObject[] inputMatrices;
    [SerializeField]
    TMP_Text result;

    float[,] tMatInput = new float[3, 3];
    Vector2 tVecInput = new Vector2();

    //HMatrix2D mat1 = new HMatrix2D();
    //HMatrix2D mat2 = new HMatrix2D();
    HMatrix2D[] mats = new HMatrix2D[2];
    HMatrix2D resultMat = new HMatrix2D();

    HVector2D vec1;
    HVector2D resultVec;

    private HMatrix2D mat = new HMatrix2D();
    // Start is called before the first frame update
    void Start()
    {
        mat.SetIdentity();
        mat.Print();
    }

    // Update is called once per frame
    void Update()
    {
        //input the values
        //run q2 after pressing enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreateMatrixArray();
            Question2();

        }
    }

    public void Question2()
    {
        //if it is matrix x matrix, print the array of array of elements of the result matrix
        string grid = "";
        if (IsMatMat())
        {
            resultMat = mats[0] * mats[1];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    //print element along with spacing 
                    grid = grid + resultMat.Entries[y, x] + "   ";
                }
                //for every new row, create a new line
                grid += "\n";

            }
        }
        else
        {
            //if it is matrix x vector, print the array of array of elements of the result vector
            resultVec = mats[0] * vec1;
            //new row between elements
            grid = resultVec.x + "\n" + resultVec.y + "\n" + resultVec.h;
        }
        result.text = grid;
     
    }

    private bool IsMatMat()
    {
        //determine if the equation is matrix x matrix or matrix x vector using the "type of matrix" inputs
        TMP_InputField[] matsUsed = FindObjectsOfType<TMP_InputField>().Where(t => t.transform.name == "type of matrix").ToArray();
        foreach (TMP_InputField mat in matsUsed)
        {
            if(mat.text == "3x1")
            {
                return false;
            }
        }
        return true;
    }

    public void CreateMatrixArray()
    {
        for (int i = 0; i < inputMatrices.Length; i++)
        {
            string typeOfMatrix = inputMatrices[i].transform.Find("type of matrix").GetComponent<TMP_InputField>().text;
            if (typeOfMatrix == "3x3")
            {
                //clear array of array (matrix)
                Array.Clear(tMatInput, 0, tMatInput.Length);

                //get input objects that are children on type of matrix object
                foreach (Transform child in inputMatrices[i].transform)
                {
                    if (child.name != "type of matrix")
                    {
                        //get the placeholder name
                        char[] pos = child.name.Replace("m", "").ToCharArray();

                        //the 1st digit is the row while the second digit is the column
                        int row = (int)char.GetNumericValue(pos[0]);
                        int col = (int)char.GetNumericValue(pos[1]);

                        //put that input of in the array of array based on the position (row and column from input object name)
                        tMatInput[row, col] = int.Parse(child.GetComponent<TMP_InputField>().text);
                    }
                }
                //add the 1 or 2 matrix inputs in a matrix array
                mats[i] = new HMatrix2D(tMatInput);
            }
            else if (typeOfMatrix == "3x1")
            {
                //get the inputs from input objects with the m00 and m01 placeholders if the type of matrix input in a vector
                tVecInput.x = int.Parse(inputMatrices[i].transform.Find("m00").GetComponent<TMP_InputField>().text);
                tVecInput.y = int.Parse(inputMatrices[i].transform.Find("m10").GetComponent<TMP_InputField>().text);
                vec1 = new HVector2D(tVecInput);
            }
        }
    }
}
