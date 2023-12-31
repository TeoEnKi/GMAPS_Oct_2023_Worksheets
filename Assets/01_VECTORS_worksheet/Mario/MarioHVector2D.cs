using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioHVector2D : MonoBehaviour
{
    public Transform planet;
    public float force = 5f;
    public float gravityStrength = 5f;

    private HVector2D gravityDir, gravityNorm;
    private HVector2D moveDir;
    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        gravityDir = new HVector2D(planet.position - transform.position);  
        moveDir = new HVector2D(gravityDir.y, -gravityDir.x);
        moveDir.Normalize();
        moveDir *= -1;
        // Your code here
        // ...

        //for the person to forward
        rb.AddForce(moveDir.ToUnityVector3() * force);

        gravityNorm = new HVector2D(gravityDir.ToUnityVector2());
        gravityNorm.Normalize();
        //for the person to stay on the planet
        Debug.Log(gravityNorm.ToUnityVector3() + "and" + gravityStrength);

        rb.AddForce(gravityNorm.ToUnityVector3() * gravityStrength);

        HVector2D forward = new HVector2D(Vector2.right);
        float angle = forward.FindAngle(moveDir)*Mathf.Rad2Deg;
        if (new HVector2D(Vector2.up).DotProduct(moveDir) < 0) 
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, -angle));
        }
        else
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, angle));

        }
        Debug.Log("angle" + angle);

        DebugExtension.DebugArrow(transform.position,
            gravityDir.ToUnityVector3(), Color.red);

        DebugExtension.DebugArrow(transform.position,
            moveDir.ToUnityVector3(), Color.blue);
        Debug.Log(Vector3.right + " + " + moveDir.ToUnityVector3() + " + " + gravityDir.ToUnityVector3());

    }
}
