using UnityEngine;
using TMPro;

public class TitleAndScoreDisplay : MonoBehaviour
{
    private TMP_Text display;

    private string title;

    private void Awake()
    {
        display = GetComponent<TMP_Text>();

        title = display.text;
    }

    public void SetTitle()
    {
        display.text = title;
    }

    public void SetScore(int score)
    {
        display.text = score.ToString();
    }
}
