using UnityEngine;
using TMPro;

public class DescriptionDisplay : MonoBehaviour
{
    private TMP_Text display;

    private string description = "Your Best Score: {0}/nMove left with A or <= and right with D or =>";

    private void Awake()
    {
        display = GetComponent<TMP_Text>();
    }

    public void SetDescription(int bestScore)
    {
        display.text = string.Format(description, bestScore);
    }

    public void ClearDescription() {
        display.text = "";
    }
}
