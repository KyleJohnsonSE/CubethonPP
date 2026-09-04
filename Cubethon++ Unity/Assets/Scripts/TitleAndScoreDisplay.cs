using UnityEngine;
using TMPro;

public class TitleAndScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text display;

    private string title = "Hole In The Wall";

    private void OnValidate() {
        if (display == null) {
            display = GetComponent<TMP_Text>();
        }
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
