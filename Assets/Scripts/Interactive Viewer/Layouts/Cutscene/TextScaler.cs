using UnityEngine;
using TMPro;

public class TextScaler: MonoBehaviour
{
    private TextMeshProUGUI text;

    public int fontSize = 24;
    private static float defaultResolution = 1327104f;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        Adjust();
    }

    private void AdjustFontSize()
    {
        float totalCurrentRes = Screen.height + Screen.width;
        float percent = totalCurrentRes / defaultResolution;
        int fontsize = Mathf.RoundToInt((float)fontSize * percent);

        text.fontSize = fontsize;
    }
}
