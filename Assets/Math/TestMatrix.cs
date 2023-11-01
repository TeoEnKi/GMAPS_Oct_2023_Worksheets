using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        //run q2 after pressingenter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreateMatrixArray();
            Question2();

        }
    }

    public void Question2()
    {
        string grid = "";
        if (IsMatMat())
        {
            resultMat = mats[0] * mats[1];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    grid = grid + resultMat.Entries[y, x] + " ";
                }
                grid += "\n";

            }
        }
        else
        {
            resultVec = mats[0] * vec1;
            grid = resultVec.x + "\n" + resultVec.y + "\n" + resultVec.h;
        }
        result.text = grid;
     
    }

    private bool IsMatMat()
    {
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
                //get object in list
                foreach (Transform child in inputMatrices[i].transform)
                {
                    if (child.name != "type of matrix")
                    {
                        char[] pos = child.name.Replace("m", "").ToCharArray();

                        int row = (int)char.GetNumericValue(pos[0]);
                        int col = (int)char.GetNumericValue(pos[1]);
                        Debug.Log(row + " and " + col + " / " + pos[0] + " and " + pos[1]);

                        tMatInput[row, col] = int.Parse(child.GetComponent<TMP_InputField>().text);
                    }
                }
                mats[i] = new HMatrix2D(tMatInput);
            }
            else if (typeOfMatrix == "3x1")
            {
                tVecInput.x = int.Parse(inputMatrices[i].transform.Find("m00").GetComponent<TMP_InputField>().text);
                tVecInput.y = int.Parse(inputMatrices[i].transform.Find("m10").GetComponent<TMP_InputField>().text);
                vec1 = new HVector2D(tVecInput);
            }
        }
    }
}
