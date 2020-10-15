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
    private ConditionalChecker cChecker;

    private Texture desiredBackground;
    private Texture desiredBackground2;

    private string desiredSpeaker;
    private string desiredSpeaker2;

    private string desiredDialogue;
    private string desiredDialogue2;

    private Asset desiredAsset;
    private Asset desiredAsset2;
    private Asset desiredAsset3;

    private Vector3 desiredAssetPosition;
    private Vector3 desiredAssetPosition2;
    private Vector3 desiredAssetPosition3;

    private List<Asset> assetList;
    private List<Asset> assetList2;

    private Cutscene desiredScene1;
    private ClickerScene desiredScene2;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);

        desiredBackground = Resources.Load<Texture>("Images/BG/Stairs");
        desiredBackground2 = Resources.Load<Texture>("Images/BG/Trees");
        desiredSpeaker = "Jesse";
        desiredDialogue = "This is a test of the <color=purple>color " +
                "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                "Now I need to make this longer to test other issues and make sure that " +
                "the line snapping is fixed by this new method. Floccinaucinihilipilification.";
        desiredSpeaker2 = "Eevee";
        desiredDialogue2 = "How did I get here?";

        desiredAssetPosition = new Vector3(130, 92);
        desiredAssetPosition2 = new Vector3(275, 147);

        desiredAsset = new Asset("TestingAsset", "CA [Eevee]", desiredAssetPosition, desiredScene1);
        desiredAsset2 = new Asset("TestingAsset2", "CA [Eevee]", desiredAssetPosition2, desiredScene2);
        assetList = new List<Asset>();

        assetList.Add(desiredAsset);
        assetList.Add(desiredAsset2);

        desiredAssetPosition3 = new Vector3(200, 150);
        desiredAsset3 = new Asset("TestingAsset3", "CA [Gem]", desiredAssetPosition3, desiredScene2);
        assetList2 = new List<Asset>();

        assetList2.Add(desiredAsset3);

        desiredScene1 = new Cutscene(desiredSpeaker2, desiredDialogue2, desiredBackground2);
        desiredScene2 = new ClickerScene(assetList2, desiredBackground2);
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

        Cutscene currentScene = new Cutscene(desiredSpeaker, desiredDialogue);

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
        Cutscene currentScene = new Cutscene(desiredSpeaker, desiredDialogue, desiredBackground);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
        Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

        Assert.AreEqual(desiredBackground, currentBackground);
    }

    [UnityTest]
    public IEnumerator testClickerSceneShowsBackground()
    {
        ClickerScene currentScene = new ClickerScene(assetList, desiredBackground);

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

        aViewer.placeInScene(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        var asset = aPanel.transform.GetChild(0);

        Assert.AreEqual(desiredAsset.getPrefab().name + "(Clone)", asset.name);

        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(asset.position.x));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(asset.position.y));
    }

    [UnityTest]
    public IEnumerator testClickerSceneShowsAssets()
    {
        ClickerScene currentScene = new ClickerScene(assetList);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        Transform asset = aPanel.transform.GetChild(0);

        Assert.AreEqual(desiredAsset.getPrefab().name + "(Clone)", asset.name);

        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(asset.position.x));
        Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(asset.position.y));
    }

    [UnityTest]
    public IEnumerator testAssetDimsOnHover()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.placeInScene(desiredAsset);
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

        aViewer.placeInScene(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject asset = GameObject.FindWithTag("Eevee");
        Image assetImage = asset.GetComponent<Image>();
        HoverListener assetHoverListener = asset.GetComponent<HoverListener>();

        assetHoverListener.Darken(assetImage);

        Assert.AreEqual(Color.grey, assetImage.color);

        assetHoverListener.Lighten(assetImage);

        Assert.AreEqual(Color.white, assetImage.color);
    }

    [UnityTest]
    public IEnumerator testCutsceneHidesDialoguePanel()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        dlViewer = eventSystem.GetComponent<DialogueViewer>();

        Cutscene currentScene = new Cutscene(desiredSpeaker, desiredDialogue);

        currentScene.show();
        yield return new WaitUntil(() => !dlViewer.getIsTyping());

        GameObject DialoguePanel = GameObject.FindWithTag("DialoguePanel");

        currentScene.hide();

        Assert.IsFalse(DialoguePanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator testDialogueViewerHidesDialoguePanel()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        dlViewer = eventSystem.GetComponent<DialogueViewer>();

        dlViewer.PrintDialogue(desiredSpeaker, desiredDialogue);
        yield return new WaitUntil(() => !dlViewer.getIsTyping());

        GameObject DialoguePanel = GameObject.FindWithTag("DialoguePanel");

        dlViewer.hideDialoguePanel();

        Assert.IsFalse(DialoguePanel.activeSelf);
    }

    [UnityTest]
    public IEnumerator testDialogueViewerClearsText()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        dlViewer = eventSystem.GetComponent<DialogueViewer>();

        Cutscene currentScene = new Cutscene(desiredSpeaker, desiredDialogue);

        currentScene.show();
        yield return new WaitUntil(() => !dlViewer.getIsTyping());

        dlViewer.PrintDialogue("", "");
        yield return new WaitForSeconds(1f);

        GameObject speakerNameText = GameObject.FindWithTag("SpeakerNameText");
        string currentSpeaker = speakerNameText.GetComponent<TMPro.TextMeshProUGUI>().text;

        Assert.AreEqual("", currentSpeaker);

        GameObject dialogueText = GameObject.FindWithTag("DialogueText");
        string currentDialogue = dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text;

        Assert.AreEqual("", currentDialogue);
    }

    [UnityTest]
    public IEnumerator testAssetViewerRemovesAssetFromScene()
    {
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.placeInScene(desiredAsset);
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        int numChildren = aPanel.transform.childCount;

        Assert.AreEqual(1, numChildren);

        aViewer.clearAssets();
        yield return new WaitForSeconds(1f);

        numChildren = aPanel.transform.childCount;

        Assert.AreEqual(0, numChildren);
    }

    [UnityTest]
    public IEnumerator testClickerSceneRemovesAssetFromScene()
    {
        ClickerScene currentScene = new ClickerScene(assetList);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        int numChildren = aPanel.transform.childCount;

        Assert.AreEqual(assetList.Count, numChildren);

        currentScene.hide();
        yield return new WaitForSeconds(1f);

        numChildren = aPanel.transform.childCount;

        Assert.AreEqual(0, numChildren);
    }

    [UnityTest]
    public IEnumerator testHasBeenClickedConditional()
    {
        ClickerScene currentScene = new ClickerScene(assetList);

        currentScene.show();
        yield return new WaitForSeconds(1f);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        GameObject asset1 = aPanel.transform.GetChild(0).gameObject;
        GameObject asset2 = aPanel.transform.GetChild(1).gameObject;

        GameObject eventSystem = GameObject.FindWithTag("EventSystem");
        aViewer = eventSystem.GetComponent<AssetViewer>();

        aViewer.handleClickedPrefab(asset1);

        cChecker = eventSystem.GetComponent<ConditionalChecker>();

        Assert.AreNotSame(asset1, asset2);
    }

    /*
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
