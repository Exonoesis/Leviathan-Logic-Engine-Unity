using UnityEngine;

public class DialogueTesting : MonoBehaviour
{
    DialogueViewer behavior;

    void Start()
    {
        behavior = DialogueViewer.Instance;
    }

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
