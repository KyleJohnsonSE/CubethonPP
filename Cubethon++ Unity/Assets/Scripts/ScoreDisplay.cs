using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text display;

    private void Awake()
    {
        display = GetComponent<TMP_Text>();
    }

    public void SetScore(string score)
    {
        display.text = score;
    }
}
