using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int wagonRoomsCount;

    public GameObject player;

    public CinemachineVirtualCamera camera;

    public int size;

    public GameObject parent;

    public GameObject room;

    public GameObject hall;

    [SerializeField]
    private ContinuousMovement wagonMovement;

    private float distanceBetweenRooms;

    private float hallOffset;

    private Vector2Int[] path;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            Generate();
    }

    private void Generate()
    {
        InitializeProperties();

        InstantiateRoom(0, 0);

        for (int i = 1; i < size; i++)
        {
            InstantiateSideRooms(i);

            for (int j = 1; j < size; j++)
            {
                InstantiateRoom(j, i);
                InstantiateHall(j, i, -hallOffset, 0);
                InstantiateHall(j, i, 0, -hallOffset);
            }
        }
    }

    private void InitializeProperties()
    {
        distanceBetweenRooms = hall.transform.localScale.x + room.transform.localScale.x;
        hallOffset = distanceBetweenRooms / 2;

        //
        // TODO: Replace with path finding algorithm.
        //
        path = new[]
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(1, 1),
            new Vector2Int(1, 2),
            new Vector2Int(2, 2)
        };

        InitializeWagon();
        InitializePlayer();
    }

    private void InitializeWagon()
    {
        ContinuousMovement wagonMovementInstance = Instantiate(wagonMovement);
        wagonMovementInstance.gameObject.SetActive(true);
        var pathNodes = new List<Vector3>(path.Length);
        foreach (Vector2Int pathNode in path)
        {
            var position = new Vector3(pathNode.x * distanceBetweenRooms, 0, pathNode.y * distanceBetweenRooms);
            pathNodes.Add(position);
        }

        wagonMovementInstance.SetupPathNodes(pathNodes);
    }

    private void InitializePlayer()
    {
        GameObject playerInstance = Instantiate(this.player);
        playerInstance.gameObject.SetActive(true);
        camera.Follow = playerInstance.transform;

        Vector2Int start = path.First();
        playerInstance.transform.position = new Vector3(start.x * distanceBetweenRooms, 1, start.y * distanceBetweenRooms);
    }

    private void InstantiateRoom(int x, int z)
    {
        var position = new Vector3(x * distanceBetweenRooms, 0, z * distanceBetweenRooms);
        InstantiateMapObject(room, position);
    }

    private void InstantiateSideRooms(int index)
    {
        InstantiateRoom(index, 0);
        InstantiateHall(index, 0, -hallOffset, 0);

        InstantiateRoom(0, index);
        InstantiateHall(0, index, 0, -hallOffset);
    }

    private void InstantiateHall(int x, int z, float xOffset, float zOffset)
    {
        var position = new Vector3(x * distanceBetweenRooms + xOffset, hall.transform.position.y, z * distanceBetweenRooms + zOffset);
        hall.transform.Rotate(0.0f, 90.0f, 0.0f);
        InstantiateMapObject(hall, position);
    }

    private void InstantiateMapObject(GameObject mapObject, Vector3 position)
    {
        Instantiate(mapObject, position, hall.transform.rotation, parent.transform);
    }
}
