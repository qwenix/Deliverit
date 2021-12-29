using UnityEngine;

public sealed class PhysicsMovement : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private CharacterController characterController;

    public void Move(Vector3 direction, float deltaTime)
    {
        Vector3 movement = direction * this.speed;

        this.characterController.SimpleMove(Vector3.ClampMagnitude(movement, this.speed));
    }
}
