using UnityEngine;

public class Cutscene : Scene
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;

    private string _speaker;
    private string _dialogue;
    private Texture _background;

    private Scene _nextScene;

    public Cutscene(string speaker, string dialogue, Texture background = null, Scene nextScene = null)
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
        bgViewer.Transition(_background);
        dlViewer.PrintDialogue(_speaker, _dialogue);
        dlViewer.setNavDest(_nextScene);
    }
    
    public override void hide()
    {
        dlViewer.clearTextFields();
        dlViewer.hideDialoguePanel();
    }

    public void setNextScene(Scene nextScene)
    {
        _nextScene = nextScene;
    }
}