using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnCollisionEnter (Collision collisionInfo)
    {
        // Stops player movement when colliding with an obstacle
        if (collisionInfo.collider.tag == "Obstacle")
        {
            gameManager.EndGame(false);
        }
    }
}
