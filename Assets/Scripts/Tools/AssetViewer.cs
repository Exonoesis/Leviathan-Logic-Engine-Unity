using System.Collections;
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

    private RectTransform caPanelRT;

    void Awake()
    {
        GameObject caPanel = GameObject.FindWithTag("AssetsPanel");
        caPanelRT = caPanel.GetComponent<RectTransform>();
    }

    public void place(Asset asset)
    {
        GameObject prefab = Resources.Load("Prefabs/" + asset.getAssetName()) as GameObject;

        GameObject prefabObject = Instantiate(prefab, caPanelRT);

        prefabObject.transform.position = asset.getPosition();
    }
}
