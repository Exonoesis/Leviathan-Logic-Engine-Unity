﻿using System.Collections.Generic;
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
        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        cChecker = ConditionalChecker.Instance;
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

        cChecker.changeSceneIfSatisfied(asset);
    }

    public Asset getAsset(GameObject prefab)
    {
        Asset asset = prefabRelations[prefab];

        return asset;
    }

    public void Darken(GameObject prefab)
    {
        Image image = prefab.GetComponent<Image>();
        image.color = Color.grey;
    }

    public void Lighten(GameObject prefab)
    {
        Image image = prefab.GetComponent<Image>();
        image.color = Color.white;
    }
}