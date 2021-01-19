using UnityEngine;

public class ButtonAsset : Asset
{
    public ButtonAsset(string prefabName, Vector3 position, Scene nextScene)
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

    public void setNextScene(Scene scene)
    {
        _nextScene = scene;
    }
}
