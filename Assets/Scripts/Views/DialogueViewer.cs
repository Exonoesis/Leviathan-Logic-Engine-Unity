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

    private AssetViewer aViewer;
    
    private GameObject DialoguePanel;
    private TextMeshProUGUI SpeakerName;
    private TextMeshProUGUI DialogueText;
    
    private Asset navButton;
    private GameObject navButtonObject;
    
    private int dialogueLength;
    public float charPrintDelay = 0.05f;
    
    private bool isTyping;
    private Coroutine typingCoroutine;

    void Awake()
    {
        aViewer = AssetViewer.Instance;
        
        DialoguePanel = GameObject.FindWithTag("DialoguePanel");
        SpeakerName = GameObject
            .FindWithTag("SpeakerNameText")
            .GetComponent<TextMeshProUGUI>();
        DialogueText = GameObject
            .FindWithTag("DialogueText")
            .GetComponent<TextMeshProUGUI>();
        navButtonObject = GameObject.FindWithTag("NavButton");
        
        navButton = new Asset(
            navButtonObject.name, 
            navButtonObject.transform.position, 
            new Button());
        
        navButton.setPrefab(navButtonObject);
        aViewer.trackCoreAsset(navButton);
        
        DialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(MouseCodes.PrimaryButton))
        {
            StopCoroutine(typingCoroutine);

            DialogueText.maxVisibleCharacters = dialogueLength;
            isTyping = false;
        }

        if (!isTyping)
        {
            navButtonObject.SetActive(true);
        }
    }

    public void PrintDialogue(string speaker, string text)
    {
        if (!DialoguePanel.activeSelf)
        {
            DialoguePanel.SetActive(true);
        }
        
        navButtonObject.SetActive(false);

        typingCoroutine = StartCoroutine(Teletype(speaker, text));
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
    
    public bool getIsTyping()
    {
        return isTyping;
    }

    public Asset getNavButton()
    {
        return navButton;
    }
    
    public void setNavDest(Scene destination)
    {
        navButton.getState().setNextScene(destination);
    }

    public void clearTextFields()
    {
        SpeakerName.text = "";
        DialogueText.text = "";
    }

    public void hideDialoguePanel()
    {
        DialoguePanel.SetActive(false);
    }
}
