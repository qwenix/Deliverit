using UnityEngine;

public class Food : MonoBehaviour
{
    public Inventory inventory;

    public Transform oldParent;

    public Sprite sprite;

    public int weight;

    public float health;

    public float healthRegeneration;

    private Time time;

    public void Drop()
    {
        oldParent.gameObject.SetActive(true);
        oldParent.transform.position = transform.position;
        transform.parent = oldParent;
        GetComponent<BoxCollider>().enabled = true;
    }
}
