using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WagonInventory : MonoBehaviour
{
    public Inventory playerInventory;

    public ContinuousMovement movement;

    public List<Food> foods;

    public List<Transform> dropPositions;

    public List<Image> images;

    public List<Text> texts;

    public Text totalText;

    public GameObject ui;

    public int maxFoods;

    public float weightMultiplier = 0.2f;

    public float height;

    public WagonInventory()
    {
        maxFoods = 8;
        height = 0.3f;
        foods = new List<Food>(maxFoods);
        for (int i = 0; i < maxFoods; i++)
        {
            foods.Add(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory otherInventory = other.GetComponent<Inventory>();
        if (otherInventory != null)
        {
            playerInventory = otherInventory;
            ui.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Inventory otherInventory = other.GetComponent<Inventory>();
        if (otherInventory != null && otherInventory == playerInventory)
        {
            playerInventory = null;
            ui.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && playerInventory != null && playerInventory.food != null)
        {
            int index = -1;
            for (int i = 0; i < maxFoods; i++)
            {
                if (foods[i] == null)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return;

            Food food = playerInventory.food;
            food.transform.position = transform.position;
            food.transform.parent = transform;

            playerInventory.DropToFloor();

            foods[index] = food;

            images[index].sprite = food.sprite;
            images[index].enabled = true;
            texts[index].text = food.weight.ToString();
            totalText.text = foods.Sum(f => f == null ? 0 : f.weight).ToString();

            float p = 2f / (maxFoods / 2f);
            float x = (index % (maxFoods / 2)) * p + p / 2f;
            float z = index / (int)(maxFoods / 2f) * 0.6f - 0.15f;

            Debug.Log(x);
            Debug.Log(z);

            food.transform.localPosition = new Vector3(x - 1f, height, z);
            food.transform.Rotate(0, Random.Range(0, 360), 0);

            movement.speed -= food.weight * weightMultiplier;
        }

        if (playerInventory != null && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.visible = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Cursor.visible = false;
        }

        if (Input.GetKey(KeyCode.G) && playerInventory != null)
        {

        }
    }

    private void Start()
    {
        Cursor.visible = false;

        for (int i = 0; i < images.Count; i++)
        {
            int i1 = i;
            images[i].GetComponent<Button>().onClick.AddListener(() => Drop(i1));
        }
    }

    private void Drop(int itemIndex)
    {
        if (!Cursor.visible)
            return;

        Food food = foods[itemIndex];
        foods[itemIndex] = null;

        food.Drop();

        food.transform.localPosition = Vector3.zero;
        food.transform.localEulerAngles = Vector3.zero;

        food.oldParent.position = dropPositions[itemIndex].position;

        movement.speed += food.weight * weightMultiplier;

        images[itemIndex].enabled = false;
        texts[itemIndex].text = string.Empty;
        totalText.text = foods.All(f => f == null) ? "" : foods.Sum(f => f == null ? 0 : f.weight).ToString();
    }
}
