using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueViewer : MonoBehaviour
{
    public static DialogueViewer instance;

    public TextMeshProUGUI SpeakerName;
    public TextMeshProUGUI DialogueText;

    public float charPrintDelay = 0.05f;

    private bool isTyping = false;
    private bool isHoldingText = false;

    private Coroutine typingCoroutine;

    private int dialogueLength;

    void Start()
    {
        instance = this; 
    }

    void Update()
    {
        if (isTyping && Input.GetKeyDown("space"))
        {
            StopCoroutine(typingCoroutine);

            DialogueText.maxVisibleCharacters = dialogueLength;
            isTyping = false;
            isHoldingText = true;
        }
    }

    public void PrintDialogue(string speaker, string text)
    {
        if (!isTyping && !isHoldingText)
        {
            typingCoroutine = StartCoroutine(Teletype(speaker, text));
        }
        
        if (isHoldingText)
        {
            isHoldingText = false;
        }
    }

    private IEnumerator Teletype(string speaker, string text)
    {
        isTyping = true;

        SpeakerName.text = speaker;
        DialogueText.text = text;
        DialogueText.maxVisibleCharacters = 0;

        dialogueLength = text.Length;
        int visibleCharCount = 0;

        while (visibleCharCount <= dialogueLength)
        {
            DialogueText.maxVisibleCharacters = visibleCharCount;

            visibleCharCount++;

            yield return new WaitForSeconds (charPrintDelay);
        }

        isTyping = false;
    }
}
