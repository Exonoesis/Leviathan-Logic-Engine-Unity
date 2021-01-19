using UnityEngine;

public class ClickerSceneAsset : Asset
{
    private int _clickedNum;

    public ClickerSceneAsset(string prefabName, Vector3 position, Scene nextScene)
    {
        _position = position;
        _nextScene = nextScene;

        _prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
    }

    public override Vector3 getPosition()
    {
        return _position;
    }

    public override GameObject getPrefab()
    {
        return _prefab;
    }

    public override Scene getNextScene()
    {
        return _nextScene;
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