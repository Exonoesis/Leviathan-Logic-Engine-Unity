public class Button : State
{
    private AssetViewer aViewer;
    private SceneNavigator sNavi;
    
    private Scene _nextScene;
    
    public override void HoverEnter(Asset asset)
    {
        aViewer = AssetViewer.Instance;
        aViewer.Darken(asset.getPrefab());
    }

    public override void HoverExit(Asset asset)
    {
        aViewer = AssetViewer.Instance;
        aViewer.Lighten(asset.getPrefab());
    }

    public override void Click(Asset asset)
    {
        sNavi = SceneNavigator.Instance;
        
        sNavi.changeSceneIfSatisfied(asset);
    }

    public override bool isClicked()
    {
        return false;
    }

    public override Scene getNextScene()
    {
        return _nextScene;
    }

    public override void setNextScene(Scene scene)
    {
        _nextScene = scene;
    }
}