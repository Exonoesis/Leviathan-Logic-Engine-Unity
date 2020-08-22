using UnityEngine;

public class ClickableObjectMethods : MonoBehaviour
{
    private DialogueViewer dlViewer;

    void Start()
    {
        dlViewer = DialogueViewer.Instance;
    }

    public void ClickedEevee()
    {
        dlViewer.PrintDialogue("Eevee", "I've been clicked!");
    }

    public void ClickedGem()
    {
        dlViewer.PrintDialogue("Gem", "I'm a gem – thus not a sentient being – but if I was I would tell you that I've been clicked.");
    }
}
