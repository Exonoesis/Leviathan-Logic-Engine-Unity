using UnityEngine;

public abstract class State
{
    public abstract void HoverEnter(Asset asset);
    public abstract void HoverExit(Asset asset);
    public abstract void Click(Asset asset);
}
