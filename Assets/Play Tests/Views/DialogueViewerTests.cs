using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Visual
{
    public class DialogueViewerTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator PrintsDialogue()
        {
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");
            DialogueViewer dlViewer = eventSystem.GetComponent<DialogueViewer>();
            
            string desiredSpeaker = "Jesse";
            string desiredDialogue = "This is a test of the <color=purple>color " +
                              "changing system</color>. It's <b>built-in</b>, and that's real <i>fancy</i>. " +
                              "Now I need to make this longer to test other issues and make sure that " +
                              "the line snapping is fixed by this new method. Floccinaucinihilipilification.";

            dlViewer.PrintDialogue(desiredSpeaker, desiredDialogue);
            yield return new WaitUntil(() => !dlViewer.getIsTyping());

            GameObject speakerNameText = GameObject.FindWithTag("SpeakerNameText");
            string currentSpeaker = speakerNameText.GetComponent<TMPro.TextMeshProUGUI>().text;

            Assert.AreEqual(desiredSpeaker, currentSpeaker);

            GameObject dialogueText = GameObject.FindWithTag("DialogueText");
            string currentDialogue = dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text;

            Assert.AreEqual(desiredDialogue, currentDialogue);
        }
    }
}