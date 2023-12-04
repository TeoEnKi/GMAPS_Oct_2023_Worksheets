using NUnit;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PoolCue : MonoBehaviour
{
    public LineFactory lineFactory;
    public GameObject ballObject;

    private Line drawnLine;
    private Ball2D ball;

    private void Start()
    {
        ball = ballObject.GetComponent<Ball2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Start line drawing
            if(ball != null && ball.IsCollidingWith(startLinePos.x,startLinePos.y))
            {
                drawnLine = lineFactory.GetLine(startLinePos, new Vector2(ball.Position.x, ball.Position.y), 1f, UnityEngine.Color.black);
                drawnLine.EnableDrawing(true);
            }
        }
        else if (Input.GetMouseButtonUp(0) && drawnLine != null)
        {
            drawnLine.EnableDrawing(false);

            //update the velocity of the white ball.
            HVector2D v = new HVector2D(ball.Position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, ball.Position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            ball.Velocity = v;

            drawnLine = null; // End line drawing            
        }

        //when dragging the mouse is outside the ball
        if (drawnLine != null)
        {
            drawnLine.start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Update line end
            drawnLine.end = new Vector2(ball.Position.x, ball.Position.y);
        }
    }

    /// <summary>
    /// Get a list of active lines and deactivates them.
    /// </summary>
    public void Clear()
    {
        var activeLines = lineFactory.GetActive();

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}
