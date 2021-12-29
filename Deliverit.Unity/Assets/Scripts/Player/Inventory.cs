using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform backTransform;

    public PhysicsMovement movement;

    public Food food;

    public Image image;

    public Text text;

    public Inventory()
    {
        PotentialFoods = new List<Food>();
    }

    private List<Food> PotentialFoods { get; }

    private void OnTriggerEnter(Collider other)
    {
        Food otherFood = other.GetComponent<Food>();
        if (otherFood != null)
        {
            PotentialFoods.Add(otherFood);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Food otherFood = other.GetComponent<Food>();
        if (otherFood != null && PotentialFoods.Contains(otherFood))
        {
            PotentialFoods.Remove(otherFood);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (food != null)
            {
                food.Drop();
                DropToFloor();
            }

            if (PotentialFoods.Count > 0)
            {
                Purchase();
            }
        }
    }

    public void DropToFloor()
    {
        food.transform.localPosition = Vector3.zero;
        food.transform.localEulerAngles = Vector3.zero;
        movement.speed += food.weight;
        food = null;
        image.sprite = null;
        image.enabled = false;
        text.text = "";
    }

    private void Purchase()
    {
        food = PotentialFoods.First();
        PotentialFoods.Remove(food);
        image.sprite = food.sprite;

        image.enabled = true;
        movement.speed -= food.weight;
        text.text = food.weight.ToString();

        food.oldParent.gameObject.SetActive(false);

        food.transform.parent = backTransform.parent;
        food.transform.position = backTransform.position;
        food.transform.rotation = backTransform.rotation;
        food.GetComponent<BoxCollider>().enabled = false;
    }
}
