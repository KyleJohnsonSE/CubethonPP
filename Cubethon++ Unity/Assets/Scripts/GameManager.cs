using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    private InputAction moveAction;

    private TitleAndScoreDisplay scoreDisplay;
    private PlayerMovement playerMovement;
    private CameraFollow cameraFollow;

    private ScoreHandler scoreHandler;
    private ResetHandler resetHandler;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        scoreDisplay = FindAnyObjectByType<TitleAndScoreDisplay>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        cameraFollow = FindAnyObjectByType<CameraFollow>();

        scoreHandler = new ScoreHandler(scoreDisplay);
        resetHandler = new ResetHandler(playerMovement, cameraFollow, scoreHandler);

        resetHandler.InitializeGame();
    }

    private void Update()
    {
        if (!resetHandler.IsResetting()) {
            // Starts the game (again) when the player inputs movement
            Vector2 movementInput = moveAction.ReadValue<Vector2>();
            if (movementInput.x != 0) {
                resetHandler.StartGame();
            }
        }
    }

    public void ResetGame() {
        StartCoroutine(resetHandler.ResetGame());
    }
}

public class ScoreHandler {
    private TitleAndScoreDisplay scoreDisplay;

    private int score = 0;
    private int bestScore = 0;

    public ScoreHandler(TitleAndScoreDisplay scoreDisplay) {
        this.scoreDisplay = scoreDisplay;
    }

    public void IncrementScore() {
        score++;
        scoreDisplay.SetScore(score);
    }

    public void ResetScore() {
        if (score > bestScore) {
            bestScore = score;
        }
        score = 0;
        scoreDisplay.SetTitle();
    }
}

public class ResetHandler {
    private PlayerMovement playerMovement;
    private CameraFollow cameraFollow;

    private ScoreHandler scoreHandler;

    private bool isResetting = false;

    public ResetHandler(PlayerMovement playerMovement, CameraFollow cameraFollow, ScoreHandler scoreHandler) {
        this.playerMovement = playerMovement;
        this.cameraFollow = cameraFollow;
        this.scoreHandler = scoreHandler;
    }

    public bool IsResetting() {
        return isResetting;
    }

    public void InitializeGame() {
        playerMovement.enabled = false;
    }

    public void StartGame() {
        if (!isResetting) {
            playerMovement.enabled = true;
        }
    }

    public IEnumerator ResetGame() {
        if (isResetting) {
            yield break;
        }
        isResetting = true;
        playerMovement.enabled = false;
        cameraFollow.enabled = false;
        
        yield return new WaitForSeconds(1.5f);

        playerMovement.ResetPos();
        cameraFollow.enabled = true;
        scoreHandler.ResetScore();
        isResetting = false;
    }
}