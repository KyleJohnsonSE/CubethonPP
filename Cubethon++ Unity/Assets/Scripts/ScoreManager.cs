using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TitleAndScoreDisplay titleAndScoreDisplay;

    private int score;
    private int bestScore;

    private void OnValidate() {
        if (titleAndScoreDisplay == null) {
            titleAndScoreDisplay = FindAnyObjectByType<TitleAndScoreDisplay>();
        }
    }    

    public int GetBestScore() {
        return bestScore;
    }

    public void ShowScore() {
        titleAndScoreDisplay.SetScore(score);
    }

    public void ShowTitle() {
        titleAndScoreDisplay.SetTitle();
    }

    public void IncrementScore() {
        score++;
        titleAndScoreDisplay.SetScore(score);
    }

    public void ResetScore() {
        if (score > bestScore) {
            bestScore = score;
        }
        score = 0;
    }
}
