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
        private string desiredSpeaker = "Jesse";
        private string desiredDialogue = "This is a test of the <color=purple>color " +
                                         "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                                         "Now I need to make this longer to test other issues and make sure that " +
                                         "the line snapping is fixed by this new method. Floccinaucinihilipilification.";
        
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
            
            Cutscene currentScene = new Cutscene(null, desiredSpeaker, desiredDialogue);

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
        public IEnumerator ShowsBackground()
        {
            Texture desiredBackground = Resources.Load<Texture>("Images/BG/Stairs");
            
            Cutscene currentScene = new Cutscene(null, desiredSpeaker, desiredDialogue, desiredBackground);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
            Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

            Assert.AreEqual(desiredBackground, currentBackground);
        }
        
        [UnityTest]
        public IEnumerator HidesDialoguePanel()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            Cutscene currentScene = new Cutscene(null, desiredSpeaker, desiredDialogue);

            currentScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            GameObject DialoguePanel = GameObject.FindWithTag("DialoguePanel");

            currentScene.hide();

            Assert.IsFalse(DialoguePanel.activeSelf);
        }
    }
}

