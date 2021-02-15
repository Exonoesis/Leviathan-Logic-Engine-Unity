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
    private SceneNavigator sNavi;
    private Dictionary<GameObject, Asset> prefabRelations;
    private Dictionary<string, GameObject> basePrefabs;

    void Awake()
    {
        sNavi = SceneNavigator.Instance;
        aPanelRT = GameObject.FindWithTag("AssetsPanel")
            .GetComponent<RectTransform>();
        
        prefabRelations = new Dictionary<GameObject, Asset>();
        basePrefabs = new Dictionary<string, GameObject>();
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

        prefabRelations.Add(prefabObject, asset);
    }

    public void clearAssets()
    {
        foreach (GameObject prefab in prefabRelations.Keys)
        {
            Destroy(prefab);
        }

        prefabRelations.Clear();
    }

    public void handleClickedPrefab(GameObject prefab)
    {
        Asset asset = prefabRelations[prefab];
        
        asset.incrementClickedNum();
        
        sNavi.changeSceneIfSatisfied(asset);
    }

    public Asset getAssetFrom(GameObject prefab)
    {
        return prefabRelations[prefab];
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