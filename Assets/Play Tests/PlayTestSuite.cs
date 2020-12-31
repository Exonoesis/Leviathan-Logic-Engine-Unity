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
    private string desiredDialogue3;

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
    private Cutscene errorScene1;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);

        
        
        
        desiredDialogue3 = "We're missing an important item.";

        
        desiredAssetPosition2 = new Vector3(275, 147);
        desiredAssetPosition3 = new Vector3(200, 150);

        string desiredSpeaker = "Eevee";
        string desiredDialogue = "How did I get here?";
        List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Stairs"),
            Resources.Load<Texture>("Images/BG/Trees")
        };
        Scene desiredScene = new Cutscene(desiredSpeaker, desiredDialogue, backgrounds[1]);
        

        assetList.Add(desiredAsset2);
        assetList2 = new List<Asset>();

        

        desiredAsset2 = new Asset("CA [Eevee]", desiredAssetPosition2, desiredScene2);
        desiredAsset3 = new Asset("CA [Gem]", desiredAssetPosition3, desiredScene2);

        
        assetList2.Add(desiredAsset3);
    }

    [TearDown]
    public void Teardown()
    {

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

        aViewer.Darken(asset);

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

        aViewer.Darken(asset);

        Assert.AreEqual(Color.grey, assetImage.color);

        aViewer.Lighten(asset);

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
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");

        aViewer = eventSystem.GetComponent<AssetViewer>();

        GameObject asset1Object = aPanel.transform.GetChild(0).gameObject;
        GameObject asset2Object = aPanel.transform.GetChild(1).gameObject;

        Asset asset1Asset = aViewer.getAsset(asset1Object);
        asset1Asset.incrementClickedNum();

        Asset asset2Asset = aViewer.getAsset(asset2Object);

        Assert.AreEqual(1, asset1Asset.getClickedNum());
        Assert.AreEqual(0, asset2Asset.getClickedNum());

        HasBeenClicked conditional1 = new HasBeenClicked(asset1Asset);
        HasBeenClicked conditional2 = new HasBeenClicked(asset2Asset);

        Assert.IsTrue(conditional1.isMet());
        Assert.IsFalse(conditional2.isMet());
    }

    [UnityTest]
    public IEnumerator testConditionalChecker0Conditionals()
    {
        desiredScene1 = new Cutscene(desiredSpeaker2, desiredDialogue2, desiredBackground2);

        desiredAsset = new Asset("CA [Eevee]", desiredAssetPosition, desiredScene1);

        assetList = new List<Asset>();
        assetList.Add(desiredAsset);

        ClickerScene currentScene = new ClickerScene(assetList, desiredBackground);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");

        cChecker = eventSystem.GetComponent<ConditionalChecker>();
        aViewer = eventSystem.GetComponent<AssetViewer>();

        currentScene.show();
        cChecker.setCurrentScene(currentScene);

        yield return new WaitForSeconds(1f);

        GameObject asset1Object = aPanel.transform.GetChild(0).gameObject;

        aViewer.handleClickedPrefab(asset1Object);

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(cChecker.getCurrentScene(), desiredScene1);
    }

    [UnityTest]
    public IEnumerator testConditionalChecker1ConditionalPass()
    {
        desiredScene1 = new Cutscene(desiredSpeaker2, desiredDialogue2, desiredBackground2);

        desiredAsset = new Asset("CA [Eevee]", desiredAssetPosition, desiredScene1);

        assetList = new List<Asset>();
        assetList.Add(desiredAsset);

        ClickerScene currentScene = new ClickerScene(assetList, desiredBackground);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");

        cChecker = eventSystem.GetComponent<ConditionalChecker>();
        aViewer = eventSystem.GetComponent<AssetViewer>();

        currentScene.show();
        cChecker.setCurrentScene(currentScene);

        HasBeenClicked condition1 = new HasBeenClicked(desiredAsset);
        List<Conditional> conditionList = new List<Conditional>();
        conditionList.Add(condition1);

        cChecker.addConditions(desiredAsset, conditionList);

        yield return new WaitForSeconds(1f);

        GameObject asset1Object = aPanel.transform.GetChild(0).gameObject;

        aViewer.handleClickedPrefab(asset1Object);

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(cChecker.getCurrentScene(), desiredScene1);
    }

    [UnityTest]
    public IEnumerator testConditionalChecker1ConditionalFail()
    {
        desiredScene1 = new Cutscene(desiredSpeaker2, desiredDialogue2, desiredBackground2);
        errorScene1 = new Cutscene(desiredSpeaker2, desiredDialogue3, desiredBackground2);

        desiredAsset = new Asset("CA [Eevee]", desiredAssetPosition, desiredScene1);
        desiredAsset2 = new Asset("CA [Eevee]", desiredAssetPosition2, desiredScene1);

        assetList = new List<Asset>();
        assetList.Add(desiredAsset);
        assetList.Add(desiredAsset2);

        ClickerScene currentScene = new ClickerScene(assetList, desiredBackground);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");

        cChecker = eventSystem.GetComponent<ConditionalChecker>();
        aViewer = eventSystem.GetComponent<AssetViewer>();

        currentScene.show();
        cChecker.setCurrentScene(currentScene);

        HasBeenClicked condition1 = new HasBeenClicked(desiredAsset2);
        List<Conditional> conditionList = new List<Conditional>();
        conditionList.Add(condition1);

        cChecker.addConditions(desiredAsset, conditionList);
        cChecker.addErrorScene(desiredAsset, condition1,errorScene1);

        yield return new WaitForSeconds(1f);

        GameObject asset1Object = aPanel.transform.GetChild(0).gameObject;

        aViewer.handleClickedPrefab(asset1Object);

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(cChecker.getCurrentScene(), errorScene1);
    }

    [UnityTest]
    public IEnumerator testConditionalChecker2Conditionals()
    {
        desiredScene1 = new Cutscene(desiredSpeaker2, desiredDialogue2, desiredBackground2);
        errorScene1 = new Cutscene(desiredSpeaker2, desiredDialogue3, desiredBackground2);
        desiredScene2 = new ClickerScene(assetList2, desiredBackground2);

        desiredAsset = new Asset("CA [Eevee]", desiredAssetPosition, desiredScene1);
        desiredAsset2 = new Asset("CA [Eevee]", desiredAssetPosition2, desiredScene1);

        assetList = new List<Asset>();
        assetList.Add(desiredAsset);
        assetList.Add(desiredAsset2);

        ClickerScene currentScene = new ClickerScene(assetList, desiredBackground);

        GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
        GameObject eventSystem = GameObject.FindWithTag("EventSystem");

        cChecker = eventSystem.GetComponent<ConditionalChecker>();
        aViewer = eventSystem.GetComponent<AssetViewer>();

        currentScene.show();
        cChecker.setCurrentScene(currentScene);

        HasBeenClicked condition1 = new HasBeenClicked(desiredAsset);
        HasBeenClicked condition2 = new HasBeenClicked(desiredAsset2);
        List<Conditional> conditionList = new List<Conditional>();
        conditionList.Add(condition1);
        conditionList.Add(condition2);

        cChecker.addConditions(desiredAsset, conditionList);


        cChecker.addErrorScene(desiredAsset, condition1, errorScene1);
        cChecker.addErrorScene(desiredAsset, condition2, desiredScene2);

        yield return new WaitForSeconds(1f);

        GameObject asset1Object = aPanel.transform.GetChild(0).gameObject;

        aViewer.handleClickedPrefab(asset1Object);

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(cChecker.getCurrentScene(), desiredScene2);
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
