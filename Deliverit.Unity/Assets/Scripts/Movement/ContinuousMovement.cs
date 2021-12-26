using System;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3[] pathNodes;

    public ContinuousMovement()
    {
        speed = 1.0f;
        pathNodes = Array.Empty<Vector3>();
    }

    public Vector3[] PathNodes
    {
        get
        {
            return pathNodes;
        }
        set
        {
            if (value == null || value == pathNodes)
            {
                return;
            }

            pathNodes = value;
        }
    }

    private Vector3 StartPosition { get; set; }

    private Vector3 EndPosition { get; set; }

    private float StartTime { get; set; }

    private int NextPathNodeIndex { get; set; }

    private float JourneyLength { get; set; }

    private void Update()
    {
        if (StartTime != 0)
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            StartMovement();
        }
    }

    private void StartMovement()
    {
        if (PathNodes.Length < 2)
        {
            return;
        }

        StartTime = Time.time;
        NextPathNodeIndex = 0;
        StartPosition = PathNodes[NextPathNodeIndex++];
        EndPosition = PathNodes[NextPathNodeIndex++];
        JourneyLength = Vector3.Distance(StartPosition, EndPosition);
    }

    private void Move()
    {
        if (transform.position == EndPosition)
        {
            if (NextPathNodeIndex == PathNodes.Length)
            {
                StartTime = 0.0f;
                return;
            }

            StartPosition = EndPosition;
            EndPosition = PathNodes[NextPathNodeIndex++];
        }

        float distanceCovered = (Time.time - StartTime) * speed;
        float fractionOfJourney = distanceCovered / JourneyLength;
        transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
    }
}
