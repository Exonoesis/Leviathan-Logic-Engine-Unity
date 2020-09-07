﻿using System.Collections.Generic;
using UnityEngine;

public class ClickerScene : Scene
{
    private BackgroundViewer bgViewer;
    private AssetViewer aViewer;

    private List<Asset> _assetList;
    private Texture _background;

    public Texture getBackground()
    {
        return _background;
    }

    public void setBackground(Texture background)
    {
        _background = background;
    }

    public List<Asset> getAsssets()
    {
        return _assetList;
    }

    public void setAssets(List<Asset> assetNames)
    {
        _assetList = assetNames;
    }

    public ClickerScene()
    {
        bgViewer = BackgroundViewer.Instance;
        aViewer = AssetViewer.Instance;
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
        foreach (Asset asset in _assetList)
        {
            aViewer.place(asset);
        }
    }
}