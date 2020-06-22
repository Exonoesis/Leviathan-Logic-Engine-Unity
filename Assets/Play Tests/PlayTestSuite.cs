using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

[TestFixture]
public class PlayTestSuite
{
    //private Game game;

    [SetUp]
    public void Setup()
    {
        //GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        //game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        //Object.Destroy(game.gameObject);
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
