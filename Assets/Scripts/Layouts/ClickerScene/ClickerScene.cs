using System.Collections.Generic;
using UnityEngine;

public class ClickerScene : Scene
{
    private BackgroundViewer bgViewer;
    private AssetViewer aViewer;

    private List<Asset> _assets;
    private Texture _background;

    public Texture getBackground()
    {
        return _background;
    }

    public void setBackground(Texture background)
    {
        _background = background;
    }

    public List<Asset> getAssets()
    {
        return _assets;
    }

    public void setAssets(List<Asset> assetNames)
    {
        _assets = assetNames;
    }

    public ClickerScene(Texture background, List<Asset> assets)
    {
        bgViewer = BackgroundViewer.Instance;
        aViewer = AssetViewer.Instance;

        _assets = assets;
        _background = background;
    }

    public override void show()
    {
        showBackground();
        showAssets();
    }

    private void showBackground()
    {
        bgViewer.Transition(_background);
    }

    private void showAssets()
    {
        foreach (Asset asset in _assets)
        {
            aViewer.place(asset);
        }
    }
}