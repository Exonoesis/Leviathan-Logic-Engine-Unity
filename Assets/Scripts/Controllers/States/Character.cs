using System.Collections.Generic;

public class Character : State
{
    private Dictionary<string, string> CharacterData;

    public Character (string speakerName, string dialogue)
    {
        CharacterData = new Dictionary<string, string>
        {
            {CharacterQueries.Name, speakerName}, 
            {CharacterQueries.Dialogue, dialogue}
        };

    }

    public Character()
    {
        
    }
    
    public override void HoverEnter(Asset asset)
    {
        
    }

    public override void HoverExit(Asset asset)
    {
        
    }

    public override void Click(Asset asset)
    {
        
    }

    public override bool isClicked()
    {
        return false;
    }

    public override string queryFor(string key)
    {
        return CharacterData[key];
    }

    public override Scene getNextScene()
    {
        return null;
    }

    public override void setNextScene(Scene scene)
    {
        
    }
}