using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameStateManager gameStateManager;

    private void OnValidate() {
        if (gameStateManager == null) {
            gameStateManager = FindAnyObjectByType<GameStateManager>();
        }
    }

    private void OnCollisionEnter (Collision collisionInfo)
    {
        // Ends the game when colliding with an obstacle
        if (collisionInfo.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            gameStateManager.EndGame();
        }
    }
}
