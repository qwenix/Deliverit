using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOfN : MonoBehaviour
{
    public   List<GameObject> items = new List<GameObject>();
    public  int neededCount;
    
 void Start()
 {
     int tmp = items.Count - neededCount;
     for (int i=0;i<tmp;i++){
            int x = Random.Range(0, items.Count-1);
            Destroy(items[x],.1f);
            items.RemoveAt(x);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
