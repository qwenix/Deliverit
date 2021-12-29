using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Text: " + other.gameObject);
        if (other.gameObject.tag == "Bridge")
        {
            Destroy(this.gameObject);
        }
    }

}
