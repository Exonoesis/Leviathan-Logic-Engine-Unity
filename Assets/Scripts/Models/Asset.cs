using UnityEngine;

public class Asset
{
    private int _clickedNum;
    private Vector3 _position;
    private Scene _nextScene;
    private string _prefabName;
    private GameObject _prefabInstance;
    private State _state;

    public Asset(string prefabName, Vector3 position, Scene nextScene, State state)
    {
        _position = position;
        _nextScene = nextScene;
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

    public Scene getNextScene()
    {
        return _nextScene;
    }

    public void setNextScene(Scene scene)
    {
        _nextScene = scene;
    }

    public State getState()
    {
        return _state;
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