using UnityEngine;

public class Cutscene
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;

    public string name;
    public Texture background;
    public string speaker;
    public string dialogue; //Should be an array for additive text?
    public string[] next;

    public Cutscene()
    {
        bgViewer = BackgroundViewer.Instance;
        dlViewer = DialogueViewer.Instance;
    }

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
        bgViewer.Transition(background);
    }

    //Print dialogue [returns choices that determine next node]
    private void showDialogue()
    {
        dlViewer.PrintDialogue(speaker, dialogue);
    }
}
