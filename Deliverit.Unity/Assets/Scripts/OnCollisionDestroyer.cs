using UnityEngine;

public class OnCollisionDestroyer : MonoBehaviour
{
    public GameObject destroyer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != destroyer)
            return;

        Destroy(gameObject);
    }
}
