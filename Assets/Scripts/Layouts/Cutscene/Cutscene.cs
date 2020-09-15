using UnityEngine;

public class Cutscene : Scene
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;

    private string _sceneName;
    private Texture _background;
    private string _speaker;
    private string _dialogue = ""; //Should be an array for additive text?

    public string getSceneName()
    {
        return _sceneName;
    }

    public void setSceneName(string sceneName)
    { 
        _sceneName = sceneName; 
    }

    public Texture getBackground()
    {
        return _background;
    }

    public void setBackground(Texture background)
    {
        _background = background;
    }

    public string getSpeaker()
    {
        return _speaker;
    }

    public void setSpeaker(string speaker)
    {
        _speaker = speaker;
    }

    public string getDialogue()
    {
        return _dialogue;
    }

    public void setDialogue(string dialogue)
    {
        _dialogue = dialogue;
    }

    public Cutscene()
    {
        bgViewer = BackgroundViewer.Instance;
        dlViewer = DialogueViewer.Instance;
    }

    public override void show()
    {
        showBackground();
        showDialogue();
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
        dlViewer.PrintDialogue("", "");
    }

    private void deactivateDialoguePanel()
    {
        dlViewer.hideDialoguePanel();
    }
}