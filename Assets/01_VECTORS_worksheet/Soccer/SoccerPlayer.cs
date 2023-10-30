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
            //AlignInCircle();
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
        Debug.Log("The minimum height is: " + num.Min());
    }

    float Magnitude(Vector3 vector)
    {
        return (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }

    Vector3 Normalise(Vector3 vector)
    {
        float mag = Magnitude(vector);
        vector.x /= mag;
        vector.y /= mag;
        vector.z /= mag;
        return vector;
    }

    float Dot(Vector3 vectorA, Vector3 vectorB)
    {
        return (vectorA.x * vectorB.x + vectorA.y * vectorB.y + vectorA.z * vectorB.z);
    }

    SoccerPlayer FindClosestPlayerDot()
    {
        SoccerPlayer closest = null;
        float minAngle = 180f;

        for (int i = 0; i < OtherPlayers.Length; i++)
        {
            Vector3 toPlayer = OtherPlayers[i].transform.position - transform.position;
            toPlayer = Normalise(toPlayer);

            float dot = Dot(transform.forward, toPlayer);
            float angle = Mathf.Acos(dot);
            angle *= Mathf.Rad2Deg;

            if (angle < minAngle)
            {
                minAngle = angle;
                closest = OtherPlayers[i];
            }
        }
        return closest;
    }

    //void AlignInCircle()
    //{
    //    float currentAngle = 0;
    //    float intervalAngle = 360 / OtherPlayers.Length;
    //    foreach (SoccerPlayer other in OtherPlayers)
    //    {
    //        return (float)Mathf.Acos(Dot(vec) / (Magnitude() * vec.Magnitude()));

    //        Vector3 otherDesPos =
    //        cos45
    //        360 / 8 = 45
    //    }
    //}
    void DrawVectors()
    {
        foreach (SoccerPlayer other in OtherPlayers)
        {
            Vector3 orgin = other.transform.position;
            foreach (SoccerPlayer notOrgin in OtherPlayers)
            {
                if (!notOrgin.IsCaptain && !other.IsCaptain)
                {
                    Vector3 dir = notOrgin.transform.position - orgin;
                    Debug.DrawRay(orgin, dir, Color.black);
                }

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

            DrawVectors();

            SoccerPlayer targetPlayer = FindClosestPlayerDot();
            targetPlayer.GetComponent<Renderer>().material.color = Color.green;

            //for the other soccer players that are no longer target, they become white
            foreach (SoccerPlayer other in OtherPlayers.Where(t => t != targetPlayer))
            {
                other.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}


