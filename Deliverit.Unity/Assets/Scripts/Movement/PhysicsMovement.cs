using UnityEngine;

public sealed class PhysicsMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private CharacterController characterController;

    public void Move(Vector3 direction, float deltaTime)
    {
        Vector3 movement = this.transform.TransformDirection(direction) * this.speed;

        this.characterController.SimpleMove(Vector3.ClampMagnitude(movement, this.speed));
    }
}
