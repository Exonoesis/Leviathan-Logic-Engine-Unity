using UnityEngine;

public class Asset : MonoBehaviour
{
    private string _assetName;
    private Vector3 _position;

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

    public void place(RectTransform rootPanel)
    {
        GameObject prefab = Resources.Load("Prefabs/" + _assetName) as GameObject;

        GameObject prefabObject = Instantiate(prefab, rootPanel);

        prefabObject.transform.position = _position;
    }
}