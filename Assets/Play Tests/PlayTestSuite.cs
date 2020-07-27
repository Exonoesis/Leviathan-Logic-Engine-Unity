using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;
using NUnit.Framework;
using System.Collections;

[TestFixture]
public class PlayTestSuite
{
    private BackgroundViewer bgViewer;
    private Texture desiredBackground;
    private string desiredSpeaker;
    private string desiredDialogue;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);

        desiredBackground = Resources.Load<Texture>("Images/BG/AXt4WfZ");
        desiredSpeaker = "Bob";
        desiredDialogue = "Hello! This is a test to see if the dialogue works.";
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
    public IEnumerator testCutsceneShowsText()
    {
        Cutscene currentScene = new Cutscene();
        currentScene.setSpeaker(desiredSpeaker);
        currentScene.setDialogue(desiredDialogue);

        currentScene.show();
        yield return new WaitForSeconds(3f);

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
    public IEnumerator testAssetShowsTextOnClicked()
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
    public IEnumerator testAssetChangesBackgroundOnClicked()
    {
        yield return null;

        Assert.Less(1, 2);
    }

    [UnityTest]
    public IEnumerator testAssetShowsOtherAssetOnClicked()
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
}
