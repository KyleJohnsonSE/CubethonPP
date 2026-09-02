using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter ()
    {
        gameManager.EndGame(true);
    }
}
