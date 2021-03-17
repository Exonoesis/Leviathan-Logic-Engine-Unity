using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Visual
{
    public class DialogueViewerTests
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
        public IEnumerator PrintsDialogue()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            dlViewer.PrintDialogue(desiredSpeaker, desiredDialogue);
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
        public IEnumerator HidesDialoguePanel()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            dlViewer.PrintDialogue(desiredSpeaker, desiredDialogue);
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            GameObject DialoguePanel = GameObject.FindWithTag("DialoguePanel");

            dlViewer.hideDialoguePanel();

            Assert.IsFalse(DialoguePanel.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator ClearsText()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            Cutscene currentScene = new Cutscene(desiredSpeaker, desiredDialogue);

            currentScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            dlViewer.clearTextFields();
            yield return new WaitForSeconds(1f);

            string currentSpeaker = GameObject
                .FindWithTag("SpeakerNameText")
                .GetComponent<TMPro.TextMeshProUGUI>()
                .text;

            Assert.AreEqual("", currentSpeaker);

            string currentDialogue = GameObject
                .FindWithTag("DialogueText")
                .GetComponent<TMPro.TextMeshProUGUI>()
                .text;

            Assert.AreEqual("", currentDialogue);
        }
    }
}