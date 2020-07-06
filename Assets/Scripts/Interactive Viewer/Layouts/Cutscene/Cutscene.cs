using UnityEngine;

public class Cutscene
{
    BackgroundViewer bgViewer;
    DialogueViewer dlViewer;

    public string sceneName { get; set; }
    public Texture background { get; set; }
    public string speaker { get; set; }
    public string dialogue { get; set; } //Should be an array for additive text?
    public string[] next { get; set; }

    public void show()
    {
        if (background != null)
        {
            showBackground();
        }

        if (dialogue != null)
        {
            showDialogue();
        }
    }

    //Fade between current and given background
    private void showBackground()
    {
        bgViewer = BackgroundViewer.instance;

        bgViewer.Transition(background);
    }

    //Print dialogue [returns choices that determine next node]
    private void showDialogue()
    {
        dlViewer = DialogueViewer.instance;

        dlViewer.PrintDialogue(speaker, dialogue);
    }
}
