using UnityEngine;

public class Cutscene : Scene
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;
    private AssetViewer aViewer;

    private (Asset speaker, Asset observer) _characters;
    private Texture _background;
    private Scene _nextScene;

    public Cutscene((Asset speaker, Asset observer) characters, Texture background = null, Scene nextScene = null)
    {
        bgViewer = BackgroundViewer.Instance;
        dlViewer = DialogueViewer.Instance;
        aViewer = AssetViewer.Instance;
        
        _characters = characters;
        _background = background;
        _nextScene = nextScene;
    }

    public override void show()
    {
        State characterData = _characters.speaker.getState();
        string characterName = characterData.queryFor(CharacterQueries.Name);
        
        bgViewer.Transition(_background);

        if (characterName != "")
        {
            aViewer.placeInScene(_characters.speaker);
        }
        
        if (_characters.observer != null)
        {
            aViewer.placeInScene(_characters.observer);
            aViewer.Darken(_characters.observer.getPrefab());
        }
        
        dlViewer.PrintDialogue(
            characterName, 
            characterData.queryFor(CharacterQueries.Dialogue));
        
        dlViewer.setNavDest(_nextScene);
    }
    
    public override void hide()
    {
        dlViewer.clearTextFields();
        dlViewer.hideDialoguePanel();
        aViewer.clearSceneAssets();
    }

    public void setNextScene(Scene nextScene)
    {
        _nextScene = nextScene;
    }
}