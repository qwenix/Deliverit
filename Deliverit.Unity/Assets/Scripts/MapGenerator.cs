using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int wagonRoomsCount;

    public int size;

    public GameObject parent;

    public GameObject room;

    public GameObject hall;

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
        distanceBetweenRooms = hall.transform.localScale.x + hall.transform.localScale.x;
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
        var position = new Vector3(x * distanceBetweenRooms + xOffset, 0.0f, z * distanceBetweenRooms + zOffset);
        hall.transform.Rotate(0.0f, 90.0f, 0.0f);
        InstantiateMapObject(hall, position);
    }

    private void InstantiateMapObject(GameObject mapObject, Vector3 position)
    {
        Instantiate(mapObject, position, hall.transform.rotation, parent.transform);
    }
}
