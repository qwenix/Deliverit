using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDors : MonoBehaviour
{
    public   List<GameObject> doors = new List<GameObject>();

    void Start()
    { int x = Random.Range(0, doors.Count);
   //  Destroy(doors[x],.1f);
    }
    public void openDor(int i)
    {
        Destroy(doors[i-1],.1f);
    }
}
