using UnityEngine;

public class Asset 
{
    private string _assetName;
    private Vector3 _position;
    private GameObject _prefab;
    private string _desiredScene;

    private int _clickedNum;

    public Asset(string assetName, string prefabName, Vector3 position, string desiredScene)
    {
        _assetName = assetName;
        _position = position;
        _desiredScene = desiredScene;

        _prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
    }

    public string getAssetName()
    {
        return _assetName;
    }

    public void setAssetName(string assetName)
    {
        _assetName = assetName;
    }

    public Vector3 getPosition()
    {
        return _position;
    }

    public void setPosition(Vector3 position)
    {
        _position = position;
    }

    public GameObject getPrefab()
    {
        return _prefab;
    }

    public void setPrefab(GameObject prefab)
    {
        _prefab = prefab;
    }

    public string getDesiredScene()
    {
        return _desiredScene;
    }

    public void setDesiredScene(string desiredScene)
    {
        _desiredScene = desiredScene;
    }

    public int getClickedNum()
    {
        return _clickedNum;
    }

    public void incrementClickedNum()
    {
        _clickedNum += 1;
    }
}