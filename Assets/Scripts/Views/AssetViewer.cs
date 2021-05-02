using System.Collections.Generic;
using System.Collections;
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

    private Coroutine moveCoroutine;
    private GameObject movingPrefab;
    private Vector2 targetPosition;
    
    private bool isMoving;
    
    void Awake()
    {
        aPanelRT = GameObject.FindWithTag("AssetsPanel")
            .GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isMoving && Input.GetMouseButtonDown(MouseCodes.PrimaryButton))
        {
            StopCoroutine(moveCoroutine);
            PlaceAt(movingPrefab, targetPosition);

            isMoving = false;
        }
    }

    public AssetViewer()
    {
        sceneAssets = new Dictionary<GameObject, Asset>();
        basePrefabs = new Dictionary<string, GameObject>();
        coreAssets = new Dictionary<GameObject, Asset>();
    }

    public void placeInScene(Asset asset)
    {
        string prefabName = asset.getPrefabName();
        GameObject assetBasePrefab;
        
        if (basePrefabs.ContainsKey(prefabName))
        {
            assetBasePrefab = basePrefabs[prefabName];
        }
        else
        {
            assetBasePrefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
            basePrefabs[prefabName] = assetBasePrefab;
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
    
    public Asset getSceneAssetFrom(GameObject prefab)
    {
        return sceneAssets[prefab];
    }
    
    public Asset getCoreAssetFrom(GameObject prefab)
    {
        return coreAssets[prefab];
    }
    
    public void clearSceneAssets()
    {
        foreach (GameObject prefab in sceneAssets.Keys)
        {
            Destroy(prefab);
        }

        sceneAssets.Clear();
    }

    public void Darken(GameObject prefab)
    {
        prefab.GetComponentInChildren<Image>().color = Color.grey;
    }

    public void Lighten(GameObject prefab)
    {
        prefab.GetComponentInChildren<Image>().color = Color.white;
    }

    public void MoveTo(GameObject prefab, Vector2 target, float speed, MovementTypes style)
    {
        moveCoroutine = StartCoroutine(MoveAsset(prefab, target, speed, style));
    }
    
    //For SmoothDamp only; higher speed = slower movement
    private IEnumerator MoveAsset(GameObject prefab, Vector2 target, float speed, MovementTypes style)
    {
        isMoving = true;
        movingPrefab = prefab;
        targetPosition = target;
        
        Vector2 currentPosition = prefab.transform.position;
        Vector2 velocity = Vector2.zero;

        speed *= Time.deltaTime;

        while (Mathf.RoundToInt(Vector2.Distance(currentPosition, target)) > 0)
        {
            currentPosition = style switch
            {
                MovementTypes.Smooth => Vector2.MoveTowards(currentPosition, target, speed * 50),
                MovementTypes.FastStart => Vector2.Lerp(currentPosition, target, speed),
                MovementTypes.FastMiddle => Vector2.SmoothDamp(currentPosition, target, ref velocity, speed * 10),
                _ => currentPosition
            };

            PlaceAt(prefab, currentPosition);
            yield return new WaitForSeconds(.001f);
        }
        
        PlaceAt(prefab, targetPosition);
        isMoving = false;
    }

    private void PlaceAt(GameObject prefab, Vector2 target)
    {
        prefab.transform.position = target;
        Asset asset = getSceneAssetFrom(prefab);
        asset.setPosition(target);
    }
    
    public bool getIsMoving()
    {
        return isMoving;
    }
}