using UnityEngine;

public class Asset 
{
    private string _assetName;
    private Vector3 _position;
    private int _clickedNum;

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

    public int getClickedNum()
    {
        return _clickedNum;
    }

    public void incrementClickedNum()
    {
        _clickedNum += 1;
    }
}