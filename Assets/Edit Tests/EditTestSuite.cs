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
    private String cutsceneText;
    private String speakerName;

    [SetUp]
    public void Setup()
    {
        background = Resources.Load<Image>("Images/UI/BG1");
        speakerName = "Bob";
        cutsceneText = "Hello! This is a test to see if the dialogue works.";

        //cutscene = new Cutscene(background, cutsceneText);
        //clickerScene = new ClickerView(background);
    }

    [TearDown]
    public void Teardown()
    {
    }

    [Test]
    public void testCutsceneShowsText()
    {
        //InteractiveViewer viewer = new InteractiveViewer();
        //viewer.addScene(cutscene);

        //viewer.showCurrentScene();

        Text shownDialogue = GameObject.FindWithTag("DialogueText").GetComponent<Text>();
        Assert.Equals(cutsceneText, shownDialogue.text);

        Text shownSpeakerName = GameObject.FindWithTag("SpeakerNameText").GetComponent<Text>();
        Assert.Equals(speakerName, shownSpeakerName.text);
    }

    [Test]
    public void testCutsceneShowsBackground()
    {
        //InteractiveViewer viewer = new InteractiveViewer();
        //viewer.addScene(cutscene);

        //viewer.showCurrentScene();

        Image shownBackground = GameObject
                                .FindWithTag("BackgroundPanel")
                                .GetComponent<Image>();
        Assert.Equals(background, shownBackground);
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
