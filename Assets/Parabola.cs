using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    Rigidbody2D rb;
    public float gravity;
    public float h;
    [SerializeField]private Transform  target;
    [SerializeField]private Transform origin;
    public bool debug = true;

    private void Start()
    {
        rb = origin.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        if (debug)
            DrawPath();
    }


    void Launch()
    {
        Physics2D.gravity = Vector2.up * gravity;
        rb.gravityScale = 1f;

        rb.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - origin.position.y;
        Vector2 displacementXZ = new Vector2(target.position.x - origin.position.x, 0);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);
        Vector2 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector2 previousDrawPoint = (Vector2)rb.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector2 displacement = launchData.initialVelocity * simulationTime + Vector2.up * gravity * simulationTime * simulationTime / 2f;
            Vector2 drawPoint = (Vector2)origin.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector2 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector2 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }


}
