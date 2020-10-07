public class HasBeenClicked : Conditional
{
    public override bool isMet(Asset asset)
    {
        if (asset.getClickedNum() > 0)
        {
            return true;
        }
        return false;
    }
}