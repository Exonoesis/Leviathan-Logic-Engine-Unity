using UnityEngine;

public class Asset
{
    private string _prefabName;
    private Vector3 _position;
    private State _state;

    private GameObject _prefabInstance;
    
    public Asset(string prefabName, Vector3 position, State state)
    {
        _prefabName = prefabName;
        _position = position;
        _state = state;
    }
    public string getPrefabName()
    {
        return _prefabName;
    }
    
    public Vector3 getPosition()
    {
        return _position;
    }

    public State getState()
    {
        return _state;
    }

    public GameObject getPrefab()
    {
        return _prefabInstance;
    }

    public void setPrefab(GameObject prefab)
    {
        _prefabInstance = prefab;
    }
}