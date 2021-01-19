public class HasBeenClicked : Conditional
{
    private ClickerSceneAsset _clickerSceneAsset;

    public HasBeenClicked(ClickerSceneAsset clickerSceneAsset)
    {
        _clickerSceneAsset = clickerSceneAsset;
    }

    public override bool isMet()
    {
        return _clickerSceneAsset.getClickedNum() > 0;
    }
}