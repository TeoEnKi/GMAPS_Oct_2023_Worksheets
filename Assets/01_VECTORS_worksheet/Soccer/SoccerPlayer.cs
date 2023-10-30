using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    public bool IsCaptain = false;
    public SoccerPlayer[] OtherPlayers;
    public float rotationSpeed = 1f;

    float angle = 0f;

    private void Start()
    {
        OtherPlayers = FindObjectsOfType<SoccerPlayer>().Where(t => t != this).ToArray();
        if (IsCaptain)
        {
            AlignInCircle();
            FindMinimum();
        }
    }

    private void FindMinimum()
    {
        List<float> num = new List<float>();
        for (int i = 0; i < 10; i++)
        {
            float height = Random.Range(5f, 20f);
            Debug.Log(height);
            num.Add(height);
        }
        Debug.Log("The minimum height is: "+ num.Min());
    }

    //float Magnitude(Vector3 vector)
    //{

    //}

    //Vector3 Normalise(Vector3 vector)
    //{

    //}

    //float Dot(Vector3 vectorA, Vector3 vectorB)
    //{

    //}

    //SoccerPlayer FindClosestPlayerDot()
    //{
    //    SoccerPlayer closest = null;

    //    return closest;
    //}

    void AlignInCircle()
    {
        float currentAngle = 0;
        float intervalAngle = 360 / OtherPlayers.Length;
        foreach (SoccerPlayer other in OtherPlayers)
        {
            //return (float)Mathf.Acos(Dot(vec) / (Magnitude() * vec.Magnitude()));

            //Vector3 otherDesPos = 
            //cos45
            //360/8=45
        }
    }
    void DrawVectors()
    {
        foreach (SoccerPlayer other in OtherPlayers)
        {
            if (!other.IsCaptain)
            {
                Vector3 dir = other.transform.position - transform.position;
                Debug.DrawRay(transform.position, dir, Color.black);
            }
        }
    }

    void Update()
    {
        DebugExtension.DebugArrow(transform.position, transform.forward, Color.red);

        if (IsCaptain)
        {
            angle += Input.GetAxis("Horizontal") * rotationSpeed;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        }
        else
        {
            DrawVectors();
        }
    }
}


