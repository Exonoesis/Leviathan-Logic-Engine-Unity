public class HasBeenClicked : Conditional
{
    private Asset _asset;

    public HasBeenClicked(Asset asset)
    {
        _asset = asset;
    }

    public override bool isMet()
    {
        return _asset.getClickedNum() > 0;
    }
}