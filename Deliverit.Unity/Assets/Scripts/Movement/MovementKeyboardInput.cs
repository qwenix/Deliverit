using System;
using UnityEngine;

internal sealed class MovementKeyboardInput : MonoBehaviour
{
    [SerializeField]
    private PhysicsMovement physicsMovement;

    [SerializeField]
    private Animator animator;

    public GameObject player;

    private Vector3 Direction { get; set; }

    private void Update()
    {
        float horizontalAxisValue = Input.GetAxis(AxisName.Horizontal);
        float verticalAxisValue = Input.GetAxis(AxisName.Vertical);

        animator.SetBool("isMoving", Math.Abs(horizontalAxisValue) + Math.Abs(verticalAxisValue) != 0.0f);

        this.Direction = new Vector3(horizontalAxisValue, 0, verticalAxisValue);
    }

    private void FixedUpdate()
    {
        player.transform.LookAt(transform.localPosition + Direction);
        this.physicsMovement.Move(this.Direction, Time.fixedDeltaTime);
    }
}
