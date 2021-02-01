using UnityEngine;

public class Asset
{
    private int _clickedNum;
    private Vector3 _position;
    private Scene _nextScene;
    private GameObject _prefab;

    public Asset(string prefabName, Vector3 position, Scene nextScene)
    {
        _position = position;
        _nextScene = nextScene;

        _prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
    }

    public Vector3 getPosition()
    {
        return _position;
    }

    public GameObject getPrefab()
    {
        return _prefab;
    }

    public Scene getNextScene()
    {
        return _nextScene;
    }

    public void setNextScene(Scene scene)
    {
        _nextScene = scene;
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