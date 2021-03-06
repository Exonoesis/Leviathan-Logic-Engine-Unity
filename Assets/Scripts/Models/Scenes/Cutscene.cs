using UnityEngine;

public class Cutscene : Scene
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;

    private Texture _background;
    private string _speaker;
    private string _dialogue = ""; //Should be an array for additive text?

    private Scene _nextScene;

    public Cutscene(Scene nextScene, string speaker, string dialogue, Texture background = null)
    {
        bgViewer = BackgroundViewer.Instance;
        dlViewer = DialogueViewer.Instance;

        _speaker = speaker;
        _dialogue = dialogue;
        _background = background;
        
        _nextScene = nextScene;
    }

    public override void show()
    {
        showBackground();
        showDialogue();
        
        dlViewer.setNavDes(_nextScene);
    }

    private void showBackground()
    {
        bgViewer.Transition(_background);
    }

    private void showDialogue()
    {
        dlViewer.PrintDialogue(_speaker, _dialogue);
    }

    public override void hide()
    {
        clearDialoguePanel();
        deactivateDialoguePanel();
    }

    private void clearDialoguePanel()
    {
        dlViewer.clearTextFields();
    }

    private void deactivateDialoguePanel()
    {
        dlViewer.hideDialoguePanel();
    }
}