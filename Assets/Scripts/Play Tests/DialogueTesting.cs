using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTesting : MonoBehaviour
{
    DisplayDialogue behavior;

    // Start is called before the first frame update
    void Start()
    {
        behavior = DisplayDialogue.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            behavior.PrintDialogue("Jesse", "This is a test of the <color=purple>color " +
                "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                "Now I need to make this longer to test other issues and make sure that " +
                "the line snapping is fixed by this new method. Floccinaucinihilipilification.");
            
        }
    }
    
}
