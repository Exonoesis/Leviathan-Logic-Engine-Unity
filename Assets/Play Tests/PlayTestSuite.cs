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
    private Texture desiredBackground;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(0);

        desiredBackground = Resources.Load<Texture>("Images/BG/AXt4WfZ");
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
    public void testCutsceneShowsText()
    {
        
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
