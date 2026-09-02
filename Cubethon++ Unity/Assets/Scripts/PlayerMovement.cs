using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction moveAction;

    private Rigidbody rigidBody;

    private float forwardSpeed = 5;
    private const float movementSpeed = 5;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Gets the horizontal component of the player input
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        float xSpeed = movementInput.x*movementSpeed;

        // Moves the player horizontally, based on player input, and forward
        rigidBody.linearVelocity = new Vector3(xSpeed, rigidBody.linearVelocity.y, forwardSpeed);
    }

    public Vector3 getPos() {
        return rigidBody.position;
    }

    public void resetToPos(Vector3 pos) {
        rigidBody.position = pos;
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.rotation = Quaternion.identity;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
