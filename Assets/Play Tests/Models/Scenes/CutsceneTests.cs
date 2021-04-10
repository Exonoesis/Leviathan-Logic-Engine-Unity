using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Visual
{
    public class CutsceneTests
    {
        private static string desiredSpeaker = "Cat";
        private static string desiredDialogue = "This is a test of the <color=purple>color " +
                                         "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                                         "Now I need to make this longer to test other issues and make sure that " +
                                         "the line snapping is fixed by this new method. Floccinaucinihilipilification.";

        private Asset cat = new Asset("CP [Cat]",
            new Vector3(0,0),
            new Character(desiredSpeaker, desiredDialogue));

        private Asset kitten = new Asset("CP [Kitten]",
            new Vector3(0, 0),
            new Character());
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator ShowsText()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            Cutscene currentScene = new Cutscene((cat, null));

            currentScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            string currentSpeaker = GameObject
                .FindWithTag("SpeakerNameText")
                .GetComponent<TMPro.TextMeshProUGUI>()
                .text;

            Assert.AreEqual(desiredSpeaker, currentSpeaker);

            string currentDialogue = GameObject
                .FindWithTag("DialogueText")
                .GetComponent<TMPro.TextMeshProUGUI>()
                .text;

            Assert.AreEqual(desiredDialogue, currentDialogue);
        }
        
        [UnityTest]
        public IEnumerator ShowsBackground()
        {
            Texture desiredBackground = Resources.Load<Texture>("Images/BG/Stairs");
            
            Cutscene currentScene = new Cutscene((cat, null), desiredBackground);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            Texture currentBackground = GameObject
                .FindWithTag("BGPanelStatic")
                .GetComponent<RawImage>()
                .texture;

            Assert.AreEqual(desiredBackground, currentBackground);
        }
        
        [UnityTest]
        public IEnumerator HidesDialoguePanel()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            Cutscene currentScene = new Cutscene((cat, null));

            currentScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            GameObject DialoguePanel = GameObject.FindWithTag("DialoguePanel");

            currentScene.hide();

            Assert.IsFalse(DialoguePanel.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlacesCharacterAsset()
        {
            Cutscene currentScene = new Cutscene((cat, null));
            currentScene.show();
            yield return new WaitForSeconds(1f);
            
            var asset = GameObject
                .FindWithTag("AssetsPanel")
                .transform
                .GetChild(0);

            Assert.AreEqual(cat.getPrefab().name, asset.name);

            Vector3 position = asset.position;
            Vector3 desiredPosition = cat.getPosition();
            
            Assert.AreEqual(Math.Floor(desiredPosition.x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(desiredPosition.y), Math.Floor(position.y));
        }

        [UnityTest]
        public IEnumerator RemovesCharacterAsset()
        {
            Cutscene currentScene = new Cutscene((cat, null));
            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(1, numAssets);

            currentScene.hide();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }

        [UnityTest]
        public IEnumerator SpeakerLightenedObserverDimmed()
        {
            Cutscene currentScene = new Cutscene((cat, kitten));
            currentScene.show();
            yield return new WaitForSeconds(1f);
            
            Image speakerAssetImage = GameObject.FindWithTag("Cat").GetComponent<Image>();
            Image obseverAssetImage = GameObject.FindWithTag("BigKitten").GetComponent<Image>();
            
            Assert.AreEqual(Color.white, speakerAssetImage.color);
            Assert.AreEqual(Color.grey, obseverAssetImage.color);
        }
        
        [UnityTest]
        public IEnumerator SpeakerIsNotDimmedWhenSolo()
        {
            Cutscene currentScene = new Cutscene((cat, null));
            currentScene.show();
            yield return new WaitForSeconds(1f);
            
            Image speakerAssetImage = GameObject.FindWithTag("Cat").GetComponent<Image>();
            
            Assert.AreEqual(Color.white, speakerAssetImage.color);
        }
    }
}