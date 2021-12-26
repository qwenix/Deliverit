using UnityEngine;

internal sealed class MovementKeyboardInput : MonoBehaviour
{
    [SerializeField]
    private PhysicsMovement physicsMovement;

    private Vector3 Direction { get; set; }

    private void Update()
    {
        float horizontalAxisValue = Input.GetAxis(AxisName.Horizontal);
        float verticalAxisValue = Input.GetAxis(AxisName.Vertical);

        this.Direction = new Vector3(horizontalAxisValue, 0, verticalAxisValue);
    }

    private void FixedUpdate()
    {
        this.physicsMovement.Move(this.Direction, Time.fixedDeltaTime);
    }
}
