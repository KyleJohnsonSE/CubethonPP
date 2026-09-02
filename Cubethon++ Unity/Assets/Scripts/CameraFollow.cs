using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Vector3 offset;

    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        offset = transform.position - playerMovement.GetPos();
    }

    private void Update()
    {
        // Maintains the same offset from the player
        transform.position = playerMovement.GetPos() + offset;
    }
}
