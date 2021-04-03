public class PaCElement : State
{
    private AssetViewer aViewer;
    private SceneNavigator sNavi;

    private Scene _nextScene;
    private bool _isClicked;

    public PaCElement(Scene nextScene)
    {
        _nextScene = nextScene;
    }
    
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
        
        _isClicked = true;
        
        sNavi.changeSceneIfSatisfied(asset);
    }
    
    public override bool isClicked()
    {
        return _isClicked;
    }

    public override string queryFor(string key)
    {
        return null;
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