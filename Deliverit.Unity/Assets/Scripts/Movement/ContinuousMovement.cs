using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private float height;

    public ContinuousMovement()
    {
        speed = 1.0f;

        PathNodes = new List<Vector3>
        {
            Vector3.zero,
            Vector3.zero,
        };
    }

    private List<Vector3> PathNodes { get; set; }

    private Vector3 EndPosition { get; set; }

    private int NextPathNodeIndex { get; set; }

    public void SetupPathNodes(List<Vector3> pathNodes)
    {
        if (pathNodes == null || pathNodes == PathNodes || pathNodes.Count < 2)
        {
            return;
        }

        PathNodes = new List<Vector3>(pathNodes.Count);
        foreach (Vector3 pathNode in pathNodes)
        {
            PathNodes.Add(new Vector3(pathNode.x, height, pathNode.z));
        }

        NextPathNodeIndex = 0;
        EndPosition = PathNodes[NextPathNodeIndex++];
        SetupNextCheckpointMovement();

        transform.localPosition = PathNodes.First();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F6))
        {
            speed += 3;
        }
    }

    private void SetupNextCheckpointMovement()
    {
        EndPosition = PathNodes[NextPathNodeIndex++];
        transform.LookAt(EndPosition);
    }

    private void Move()
    {
        if (transform.position == EndPosition)
        {
            if (NextPathNodeIndex == PathNodes.Count)
            {
                speed = 0f;
                return;
            }

            SetupNextCheckpointMovement();
        }

        transform.position = Vector3.MoveTowards(transform.position, EndPosition, speed * Time.deltaTime);
    }
}
