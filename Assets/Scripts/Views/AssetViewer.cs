using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetViewer : MonoBehaviour
{
    private static AssetViewer _instance;
    public static AssetViewer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AssetViewer>();
            }
            return _instance;
        }
    }

    private RectTransform aPanelRT;
    
    private Dictionary<string, GameObject> basePrefabs;
    
    //Scene Assets are temporary and belong to the currently loaded Scene.
    //The Dictionary is populated when a Scene containing Assets is loaded and is cleared upon scene change.
    private Dictionary<GameObject, Asset> sceneAssets;
    
    //Core Assets are persistent throughout the game.
    //The Dictionary is populated at game initialization and is not cleared during runtime.
    private Dictionary<GameObject, Asset> coreAssets;

    void Awake()
    {
        aPanelRT = GameObject.FindWithTag("AssetsPanel")
            .GetComponent<RectTransform>();
    }
    
    public AssetViewer()
    {
        sceneAssets = new Dictionary<GameObject, Asset>();
        basePrefabs = new Dictionary<string, GameObject>();
        coreAssets = new Dictionary<GameObject, Asset>();
    }

    public void placeInScene(Asset asset)
    {
        GameObject assetBasePrefab;
        if (basePrefabs.ContainsKey(asset.getPrefabName()))
        {
            assetBasePrefab = basePrefabs[asset.getPrefabName()];
        }
        else
        {
            assetBasePrefab = Resources.Load("Prefabs/" + asset.getPrefabName()) as GameObject;
            basePrefabs[asset.getPrefabName()] = assetBasePrefab;
        }

        GameObject prefabObject = Instantiate(assetBasePrefab, aPanelRT);

        prefabObject.transform.position = asset.getPosition();
        
        asset.setPrefab(prefabObject);

        sceneAssets.Add(prefabObject, asset);
    }

    public void trackCoreAsset(Asset asset)
    {
        coreAssets.Add(asset.getPrefab(), asset);
    }

    public void clearAssets()
    {
        foreach (GameObject prefab in sceneAssets.Keys)
        {
            Destroy(prefab);
        }

        sceneAssets.Clear();
    }
    
    public Asset getSceneAssetFrom(GameObject prefab)
    {
        return sceneAssets[prefab];
    }
    
    public Asset getCoreAssetFrom(GameObject prefab)
    {
        return coreAssets[prefab];
    }

    public void Darken(GameObject prefab)
    {
        prefab.GetComponentInChildren<Image>().color = Color.grey;
    }

    public void Lighten(GameObject prefab)
    {
        prefab.GetComponentInChildren<Image>().color = Color.white;
    }
}