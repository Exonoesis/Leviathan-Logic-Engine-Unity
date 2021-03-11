public abstract class State
{
    //Player Actions
    public abstract void HoverEnter(Asset asset);
    public abstract void HoverExit(Asset asset);
    public abstract void Click(Asset asset);
    
    //Queries
    public abstract bool isClicked();
    public abstract Scene getNextScene();
    
    //Record Keeping
    public abstract void setNextScene(Scene scene);
}
