using UnityEngine;

public class ClickerSceneObject : State
{
    private AssetViewer aViewer;
    
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
        
    }
}