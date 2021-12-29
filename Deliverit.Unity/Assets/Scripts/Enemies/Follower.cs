using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public NavMeshAgent enemy;

    public Transform follower;

    void Update()
    {
        enemy.SetDestination(follower.position);
    }
}
