using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThrowPredictor : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 force;
    public float timeStep = 0.05f;
    public int maxSteps = 500;

    private LineRenderer lineRenderer;

    public void DrawTrajectoryLine(Rigidbody rigidbody, Vector3 forceVector)
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = rigidbody;
        force = forceVector;

        Vector3[] trajectoryPoints = PredictTrajectory(rb, force);
        DrawTrajectory(trajectoryPoints);

        Vector3 landingPoint = GetLandingPoint(trajectoryPoints);
    }

    private Vector3[] PredictTrajectory(Rigidbody rigidbody, Vector3 appliedForce)
    {
        Vector3 velocity = appliedForce / rigidbody.mass;
        Vector3 position = rigidbody.position;
        Vector3 gravity = Physics.gravity;

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < maxSteps; i++)
        {
            points.Add(position);

            // Apply physics update
            velocity += gravity * timeStep;
            position += velocity * timeStep;

            if (position.y <= 0f)
            {
                position.y = 0f;
                points.Add(position);
                break;
            }
        }

        return points.ToArray();
    }

    private Vector3 GetLandingPoint(Vector3[] points)
    {
        return points[points.Length - 1];
    }

    private void DrawTrajectory(Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    public void EraseLine()
    {
        lineRenderer.positionCount = 0;
        
    }
}
