using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction moveAction;

    private Rigidbody rigidBody;

    private float forwardSpeed = 5;
    private const float movementSpeed = 5;
    private Vector3 startPos;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody>();

        startPos = rigidBody.position;
    }

    private void FixedUpdate()
    {
        // Gets the horizontal component of the player input
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        float xSpeed = movementInput.x*movementSpeed;

        // Moves the player forward and horizontally, based on player input
        rigidBody.linearVelocity = new Vector3(xSpeed, rigidBody.linearVelocity.y, forwardSpeed);
    }

    public Vector3 GetPos() {
        return rigidBody.position;
    }

    public void ResetPos() {
        rigidBody.position = startPos;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.rotation = Quaternion.identity;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
