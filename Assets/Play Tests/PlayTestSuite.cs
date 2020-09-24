using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;

//Pause
//yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.N));

[TestFixture]
public class PlayTestSuite
{
    private BackgroundViewer bgViewer;
    private DialogueViewer dlViewer;
    private AssetViewer aViewer;

    private Texture desiredBackground;
    private string desiredSpeaker;
    private string desiredDialogue;

    private Asset desiredAsset;
    private Vector3 desiredAssetPosition;
    private List<Asset> assetList;

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

        desiredAsset = new Asset();
        assetList = new List<Asset>();

        desiredAsset.setAssetName("CA [Eevee]");
        desiredAssetPosition = new Vector3(0, 0, 0);
        desiredAsset.setPosition(desiredAssetPosition);

        assetList.Add(desiredAsset);
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
        yield return new WaitUntil(() => !dlViewer.getIsTyping());

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
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        dlViewer = eventSystem.GetComponent<DialogueViewer>();

        Cutscene currentScene = new Cutscene();
        currentScene.setSpeaker(desiredSpeaker);
        currentScene.setDialogue(desiredDialogue);

        currentScene.show();
        yield return new WaitUntil(() => !dlViewer.getIsTyping());

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
        ClickerScene currentScene = new ClickerScene(desiredBackground, assetList);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
        Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

        Assert.AreEqual(desiredBackground, currentBackground);
    }

    [UnityTest]
    public IEnumerator testAssetViewerPlacesAsset()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.place(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        var asset = aPanel.transform.GetChild(0);

        Assert.AreEqual(desiredAsset.getAssetName() + "(Clone)", asset.name);

        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(asset.position.x));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(asset.position.y));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().z), Math.Floor(asset.position.z));
    }

    [UnityTest]
    public IEnumerator testClickerSceneShowsAssets()
    {
        ClickerScene currentScene = new ClickerScene(desiredBackground, assetList);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        Transform asset = aPanel.transform.GetChild(0);

        Assert.AreEqual(desiredAsset.getAssetName() + "(Clone)", asset.name);

        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(asset.position.x));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(asset.position.y));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().z), Math.Floor(asset.position.z));
    }

    [UnityTest]
    public IEnumerator testAssetDimsOnHover()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.place(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject asset = GameObject.FindWithTag("Eevee");
        Image assetImage = asset.GetComponent<Image>();
        HoverListener assetHoverListener = asset.GetComponent<HoverListener>();

        Assert.AreEqual(Color.white, assetImage.color);

        assetHoverListener.Darken(assetImage);

        Assert.AreEqual(Color.grey, assetImage.color);
    }

    [UnityTest]
    public IEnumerator testAssetLightensOnHoverExit()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.place(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject asset = GameObject.FindWithTag("Eevee");
        Image assetImage = asset.GetComponent<Image>();
        HoverListener assetHoverListener = asset.GetComponent<HoverListener>();

        assetHoverListener.Darken(assetImage);

        Assert.AreEqual(Color.grey, assetImage.color);

        assetHoverListener.Lighten(assetImage);

        Assert.AreEqual(Color.white, assetImage.color);
    }

    /*
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
