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
    }
}

