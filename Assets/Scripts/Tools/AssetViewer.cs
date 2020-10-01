using System.Collections.Generic;
using UnityEngine;

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
    private Dictionary<GameObject, Asset> prefabRelations;

    void Awake()
    {
        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        aPanelRT = aPanel.GetComponent<RectTransform>();

        prefabRelations = new Dictionary<GameObject, Asset>();
    }

    public void placeInScene(Asset asset)
    {
        GameObject prefab = asset.getPrefab();

        GameObject prefabObject = Instantiate(prefab, aPanelRT);

        prefabObject.transform.position = asset.getPosition();

        prefabRelations.Add(prefabObject, asset);
    }

    public void clearAssets()
    {
        foreach (KeyValuePair<GameObject,Asset> entry in prefabRelations)
        {
            Destroy(entry.Key);
        }

        prefabRelations.Clear();
    }

    public void handleClickedPrefab(GameObject prefab)
    {
        Asset asset = prefabRelations[prefab];

        asset.incrementClickedNum();

        print("Clicked number: " + asset.getClickedNum());

        //Send Asset to Conditional Checker                   
    }
}