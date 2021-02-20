using UnityEngine;

public class Asset
{
    private Vector3 _position;
    private string _prefabName;
    private GameObject _prefabInstance;
    private State _state;

    public Asset(string prefabName, Vector3 position, State state)
    {
        _position = position;
        _state = state;
        _prefabName = prefabName;
    }

    public Vector3 getPosition()
    {
        return _position;
    }

    public string getPrefabName()
    {
        return _prefabName;
    }

    public GameObject getPrefab()
    {
        return _prefabInstance;
    }

    public void setPrefab(GameObject prefab)
    {
        _prefabInstance = prefab;
    }

    public State getState()
    {
        return _state;
    }
}