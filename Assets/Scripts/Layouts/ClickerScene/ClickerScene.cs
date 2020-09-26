using System.Collections.Generic;
using UnityEngine;

public class ClickerScene : Scene
{
    private BackgroundViewer bgViewer;
    private AssetViewer aViewer;
    private GameObject aPanel;

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
        aPanel = GameObject.FindWithTag("AssetsPanel");

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
            aViewer.placeInScene(asset);
        }
    }

    public override void hide()
    {
        removeAssets();
    }

    private void removeAssets()
    {
        List<GameObject> childrenToRemove = new List<GameObject>();
        int numChildren;

        numChildren = aPanel.transform.childCount;

        for (int i = 0; i < numChildren; i++)
        {
            childrenToRemove.Add(aPanel.transform.GetChild(i).gameObject);
        }

        foreach (GameObject child in childrenToRemove)
        {
            aViewer.removeFromScene(child);
        }
    }
}