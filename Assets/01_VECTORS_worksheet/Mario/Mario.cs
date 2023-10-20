using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Transform planet;
    public float force = 5f;
    public float gravityStrength = 5f;

    private Vector3 gravityDir, gravityNorm;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //orgin is person
        gravityDir = planet.position - transform.position;
        moveDir = new Vector3(gravityDir.y,-gravityDir.x, 0f);
        //for the astronaut to move clockwise
        moveDir = moveDir.normalized * -1f;

        rb.AddForce(moveDir * force);

        gravityNorm = gravityDir.normalized;
        rb.AddForce(gravityNorm * gravityStrength);

        float angle = Vector3.SignedAngle(transform.position,
            moveDir, Vector3.forward);

        rb.MoveRotation(Quaternion.Euler(0, 0, angle));

        DebugExtension.DebugArrow(transform.position,
            gravityDir, Color.red);

        DebugExtension.DebugArrow(transform.position,
            moveDir, Color.blue);
        Debug.Log(moveDir + " + " +gravityDir);
    }
}


