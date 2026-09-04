using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Threading.Tasks;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private InputAction moveAction;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private CameraFollow cameraFollow;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private DescriptionDisplay descriptionDisplay;

    private GameStateContext context;
    private IGameState currentState;

    private void OnValidate() {
        if (playerMovement == null) {
            playerMovement = FindAnyObjectByType<PlayerMovement>();
        }
        if (cameraFollow == null) {
            cameraFollow = FindAnyObjectByType<CameraFollow>();
        }
        if (scoreManager == null) {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }
        if (descriptionDisplay == null) {
            descriptionDisplay = FindAnyObjectByType<DescriptionDisplay>();
        }
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        context = new GameStateContext(
            this,
            moveAction,
            playerMovement,
            cameraFollow,
            scoreManager,
            descriptionDisplay
        );
        currentState = new GameStart(context);
        currentState.OnEnter();
    }

    private void FixedUpdate()
    {
        currentState.UpdateState();
    }

    public void ChangeState(IGameState newState) {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    public void StartGame() {
        ChangeState(new GameRunning(context));
    }

    public void EndGame() {
        if (!(currentState is GameEnd)) {
            ChangeState(new GameEnd(context));
        }
    }

    public void ResetGame() {
        ChangeState(new GameStart(context));
    }
}

public class GameStateContext
{
    public GameStateManager stateManager { get; }
    public InputAction moveAction { get; }
    public PlayerMovement playerMovement { get; }
    public CameraFollow cameraFollow { get; }
    public ScoreManager scoreManager { get; }
    public DescriptionDisplay descriptionDisplay { get; }

    public GameStateContext(
        GameStateManager stateManager,
        InputAction moveAction,
        PlayerMovement playerMovement,
        CameraFollow cameraFollow,
        ScoreManager scoreManager,
        DescriptionDisplay descriptionDisplay)
    {
        this.stateManager = stateManager;
        this.moveAction = moveAction;
        this.playerMovement = playerMovement;
        this.cameraFollow = cameraFollow;
        this.scoreManager = scoreManager;
        this.descriptionDisplay = descriptionDisplay;
    }
}

public interface IGameState {
    void OnEnter();
    void UpdateState();
    void OnExit();
}

public class GameStart : IGameState {
    private readonly GameStateContext context;

    public GameStart(GameStateContext context) {
        this.context = context;
    }

    public void OnEnter() {
        context.playerMovement.enabled = false;
        context.scoreManager.ShowTitle();
        context.descriptionDisplay.SetDescription(context.scoreManager.GetBestScore());
    }

    public void UpdateState() {
        Vector2 movementInput = context.moveAction.ReadValue<Vector2>();
        if (movementInput.x != 0) {
            context.stateManager.StartGame();
        }
    }

    public void OnExit() {
        context.playerMovement.enabled = true;
        context.scoreManager.ShowScore();
        context.descriptionDisplay.ClearDescription();
    }
}

public class GameRunning : IGameState {
    private readonly GameStateContext context;

    public GameRunning(GameStateContext context) {
        this.context = context;
    }

    public void OnEnter() {}

    public void UpdateState() {}

    public void OnExit() {}
}

public class GameEnd : IGameState {
    private readonly GameStateContext context;

    private float endTime;

    public GameEnd(GameStateContext context) {
        this.context = context;
    }

    public void OnEnter() {
        endTime = Time.time;
        context.playerMovement.enabled = false;
        context.cameraFollow.enabled = false;
    }

    public void UpdateState() {
        if (Time.time - endTime >= 1.5f) {
            context.stateManager.ResetGame();
        }
    }

    public void OnExit() {
        context.playerMovement.ResetKinematics();
        context.cameraFollow.enabled = true;
        context.scoreManager.ResetScore();
    }
}