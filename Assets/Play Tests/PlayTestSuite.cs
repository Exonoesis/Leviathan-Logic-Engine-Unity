using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

[TestFixture]
public class PlayTestSuite
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;
    private Texture desiredBackground;
    private string desiredSpeaker;
    private string desiredDialogue;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);

        desiredBackground = Resources.Load<Texture>("Images/BG/AXt4WfZ");
        desiredSpeaker = "Jesse";
        desiredDialogue = "This is a test of the <color=purple>color " +
                "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                "Now I need to make this longer to test other issues and make sure that " +
                "the line snapping is fixed by this new method. Floccinaucinihilipilification.";
    }

    [TearDown]
    public void Teardown()
    {
        
    }

    [UnityTest]
    public IEnumerator testBackgroundViewerTransition()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        bgViewer = eventSystem.GetComponent<BackgroundViewer>();

        bgViewer.Transition(desiredBackground);
        yield return new WaitForSeconds(1f);

        GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
        Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

        Assert.AreEqual(desiredBackground, currentBackground);
    }

    [UnityTest]
    public IEnumerator testDialogueViewerPrintsDialogue()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        dlViewer = eventSystem.GetComponent<DialogueViewer>();

        dlViewer.PrintDialogue(desiredSpeaker, desiredDialogue);
        yield return new WaitForSeconds(15f);

        GameObject speakerNameText = GameObject.FindWithTag("SpeakerNameText");
        string currentSpeaker = speakerNameText.GetComponent<TMPro.TextMeshProUGUI>().text;
        
        Assert.AreEqual(desiredSpeaker, currentSpeaker);
        
        GameObject dialogueText = GameObject.FindWithTag("DialogueText");
        string currentDialogue = dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text;

        Assert.AreEqual(desiredDialogue, currentDialogue);
    }

    [UnityTest]
    public IEnumerator testCutsceneShowsText()
    {
        Cutscene currentScene = new Cutscene();
        currentScene.setSpeaker(desiredSpeaker);
        currentScene.setDialogue(desiredDialogue);

        currentScene.show();
        yield return new WaitForSeconds(15f);

        GameObject speakerNameText = GameObject.FindWithTag("SpeakerNameText");
        string currentSpeaker = speakerNameText.GetComponent<TMPro.TextMeshProUGUI>().text;
        
        Assert.AreEqual(desiredSpeaker, currentSpeaker);
        
        GameObject dialogueText = GameObject.FindWithTag("DialogueText");
        string currentDialogue = dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text;
        
        Assert.AreEqual(desiredDialogue, currentDialogue);
    }

    [UnityTest]
    public IEnumerator testCutsceneShowsBackground()
    {
        Cutscene currentScene = new Cutscene();
        currentScene.setBackground(desiredBackground);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
        Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

        Assert.AreEqual(desiredBackground, currentBackground);
    }

    [UnityTest]
    public IEnumerator testClickerSceneShowsBackground()
    {
        ClickerScene currentScene = new ClickerScene();
        currentScene.setBackground(desiredBackground);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
        Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

        Assert.AreEqual(desiredBackground, currentBackground);
    }

    /*
    [UnityTest]
    public IEnumerator testClickerSceneShowsAssets()
    {
        yield return null;

        Assert.Less(1, 2);
    }   

    [UnityTest]
    public IEnumerator testAssetIsGlowingWhenNotClickedOnYet()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public IEnumerator testAssetStopsGlowingOnClicked()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public IEnumerator testAssetTriggersCutsceneOnClicked()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public IEnumerator testAssetTriggersClickerSceneOnClicked()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public void testNextSceneFromCutsceneToClicker()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public void testNextSceneFromClickerToCutscene()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public void testNextSceneFromCutsceneToCutscene()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public void testNextSceneFromClickerToClicker()
    {
        yield return null;

        Assert.Less(1, 2);
    }
    */
}
