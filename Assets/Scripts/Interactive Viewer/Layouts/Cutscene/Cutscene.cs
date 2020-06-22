using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene
{
    BackgroundTransition TransitionFile;

    Canvas backgroundPanel;
    Canvas dialoguePanel;

    string name;
    Texture background;
    string speaker;
    string dialogue; //Should be an array for additive text?
    string[] next; 

    public void show()
    {
        if (background != null)
        {
            showBackground();
        }

        if (dialogue != null)
        {
            dialoguePanel.enabled = true;
            showDialogue();
        }
    }

    //Fade between current and given background
    //This function needs testing still
    private void showBackground()
    {
        TransitionFile = BackgroundTransition.instance;
 
        TransitionFile.Transition(background);
    }

    //Print dialogue [returns choices that determine next node]
    private void showDialogue()
    {
        
    }
}
