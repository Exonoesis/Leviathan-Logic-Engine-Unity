﻿using System.Collections.Generic;
using UnityEngine;

public class PointandClick : Scene
{
    private BackgroundViewer bgViewer;
    private AssetViewer aViewer;

    private List<Asset> _assets;
    private Texture _background;

    public PointandClick(List<Asset> assets, Texture background = null)
    {
        bgViewer = BackgroundViewer.Instance;
        aViewer = AssetViewer.Instance;

        _assets = assets;
        _background = background;
    }

    public override void show()
    {
        bgViewer.Transition(_background);
        
        foreach (Asset asset in _assets)
        {
            aViewer.placeInScene(asset);
        }
    }
    
    public override void hide()
    {
        aViewer.clearSceneAssets();
    }
}