using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerScene : Scene
{
    private BackgroundViewer bgViewer;

    //private List<> assetList;
    private Texture _background;

    public Texture getBackground()
    {
        return _background;
    }

    public void setBackground(Texture background)
    {
        _background = background;
    }

    public ClickerScene()
    {
        bgViewer = BackgroundViewer.Instance;
        //???
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
        //For each asset in assetList, call placeAsset(asset);
    }

    private void placeAsset()
    {
        //???
    }
}
