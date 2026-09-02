using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnCollisionEnter (Collision collisionInfo)
    {
        // Ends the game when colliding with an obstacle
        if (collisionInfo.collider.tag == "Obstacle")
        {
            gameManager.ResetGame();
        }
    }
}
