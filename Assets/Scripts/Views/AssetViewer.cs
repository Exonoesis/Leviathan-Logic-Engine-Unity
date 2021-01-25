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
    private ConditionalChecker cChecker;
    private Dictionary<GameObject, Asset> prefabRelations;

    void Awake()
    {
        cChecker = ConditionalChecker.Instance;
        aPanelRT = GameObject.FindWithTag("AssetsPanel")
            .GetComponent<RectTransform>();
        
        prefabRelations = new Dictionary<GameObject, Asset>();
    }

    public void placeInScene(Asset asset)
    {
        GameObject prefabObject = Instantiate(asset.getPrefab(), aPanelRT);

        prefabObject.transform.position = asset.getPosition();

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
        Asset rawAsset = prefabRelations[prefab];
        
        if (rawAsset is ClickerSceneAsset)
        {
            print("ClickerSceneAsset");
            ClickerSceneAsset asset = (ClickerSceneAsset) rawAsset;
            asset.incrementClickedNum();
            cChecker.changeSceneIfSatisfied(asset);
        }
        else if (rawAsset is ButtonAsset)
        {
            cChecker.changeSceneIfSatisfied(rawAsset);
        }
    }

    public Asset getAsset(GameObject prefab)
    {
        return prefabRelations[prefab];
    }

    public void Darken(GameObject prefab)
    {
        prefab.GetComponent<Image>().color = Color.grey;
    }

    public void Lighten(GameObject prefab)
    {
        prefab.GetComponent<Image>().color = Color.white;
    }
}