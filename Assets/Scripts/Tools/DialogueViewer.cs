using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueViewer : MonoBehaviour
{
    private static DialogueViewer _instance;
    public static DialogueViewer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueViewer>();
            }

            return _instance;
        }
    }

    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerName;
    public TextMeshProUGUI DialogueText;

    public float charPrintDelay = 0.05f;

    private bool isTyping;
    private bool isHoldingText;

    private Coroutine typingCoroutine;

    private int dialogueLength;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(MouseCodes.PrimaryButton))
        {
            StopCoroutine(typingCoroutine);

            DialogueText.maxVisibleCharacters = dialogueLength;
            isTyping = false;
            isHoldingText = true;
        }

        if (isHoldingText && Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
        {
            isHoldingText = false;
        }
    }

    public void PrintDialogue(string speaker, string text)
    {
        if (text != "")
        {
            if (!DialoguePanel.activeSelf)
            {
                DialoguePanel.SetActive(true);
            }

            if (!isTyping && !isHoldingText)
            {
                typingCoroutine = StartCoroutine(Teletype(speaker, text));
            }
        }
        else
        {
            clearTextFields(speaker, text);
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

    private void clearTextFields(string speaker, string text)
    {
        SpeakerName.text = speaker;
        DialogueText.text = text;
    }

    public void hideDialoguePanel()
    {
        DialoguePanel.SetActive(false);
    }
}
