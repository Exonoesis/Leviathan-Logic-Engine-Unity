using UnityEngine;

public class Asset 
{
    private Vector3 _position;
    private GameObject _prefab;
    private Scene _desiredScene;

    private int _clickedNum;

    public Asset(string prefabName, Vector3 position, Scene desiredScene)
    {
        _position = position;
        _desiredScene = desiredScene;

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

    public Scene getDesiredScene()
    {
        return _desiredScene;
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