using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;
using System;

//using InteractiveLayouts;

[TestFixture]
public class EditTestSuite
{
    //private ClickerView clickerScene;
    //private Cutscene cutscene;

    private Image background;

    [SetUp]
    public void Setup()
    {
        background = Resources.Load<Image>("Images/UI/BG1");


        //cutscene = new Cutscene(background, cutsceneText);
        //clickerScene = new ClickerView(background);
    }

    [TearDown]
    public void Teardown()
    {
    }

    [Test]
    public void testClickerSceneShowsAssets()
    {

    }

    [Test]
    public void testClickerSceneShowsBackground()
    {
        //InteractiveViewer viewer = new InteractiveViewer();
        //viewer.addScene(clickerScene);

        //viewer.showCurrentScene();

        Image shownBackground = GameObject
                                .FindWithTag("BackgroundPanel")
                                .GetComponent<Image>();
        Assert.Equals(background, shownBackground);
    }

    [Test]
    public void testNextSceneFromCutsceneToClicker()
    {

    }

    [Test]
    public void testNextSceneFromClickerToCutscene()
    {

    }

    [Test]
    public void testNextSceneFromCutsceneToCutscene()
    {

    }

    [Test]
    public void testNextSceneFromClickerToClicker()
    {

    }

    [Test]
    public void testPreviousSceneFromCutsceneToClicker()
    {

    }

    [Test]
    public void testPreviousSceneFromClickerToCutscene()
    {

    }

    [Test]
    public void testPreviousSceneFromCutsceneToCutscene()
    {

    }

    [Test]
    public void testPreviousSceneFromClickerToClicker()
    {

    }
}
