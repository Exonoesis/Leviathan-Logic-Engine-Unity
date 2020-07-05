using UnityEngine;

public class Cutscene
{
    BackgroundViewer bgViewer;
    DialogueViewer dlViewer;

    public string name;
    public Texture background;
    public string speaker;
    public string dialogue; //Should be an array for additive text?
    public string[] next; 

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
