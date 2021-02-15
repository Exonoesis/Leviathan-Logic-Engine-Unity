using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections.Generic;

namespace Visual
{
    public class SceneTransitionTests
    {
        private string speaker1 = "Cat";
        private string dialogue1 = "While I may be soft and cute, I'm also lost and scared. " +
                                   "I don't know where I am, what's going on, or how I got here.";
        
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };
        
        private List<Asset> assets1 = new List<Asset> 
        {
            new Asset("CA [Cat]",
                new Vector3(130, 92), 
                null,
                new ClickerSceneObject())
        };
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator CutsceneCutscene()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            string speaker2 = "Jesse";
            string dialogue2 = "That's very unfortunate, small cat. I wish I could help you out there. "
                               + "However this is only a test and soon we will vanish.";
            
            Cutscene firstScene = new Cutscene(speaker1, dialogue1, backgrounds[0]);
            Cutscene secondScene = new Cutscene(speaker2, dialogue2, backgrounds[1]);

            firstScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            firstScene.hide();
            secondScene.show();
            
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator CutsceneClicker()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            Cutscene firstScene = new Cutscene(speaker1, dialogue1, backgrounds[1]);
            ClickerScene secondScene = new ClickerScene(assets1, backgrounds[0]);
            
            firstScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            firstScene.hide();
            secondScene.show();
            yield return new WaitForSeconds(3f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerCutscene()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            ClickerScene firstScene = new ClickerScene(assets1, backgrounds[0]);
            Cutscene secondScene = new Cutscene(speaker1, dialogue1, backgrounds[1]);
            
            firstScene.show();
            yield return new WaitForSeconds(3f);
            
            firstScene.hide();
            secondScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerClicker()
        {
            List<Asset> assets2 = new List<Asset> 
            {
                new Asset("CA [Cat]",
                    new Vector3(270, 132), 
                    null,
                    new ClickerSceneObject())
            };

            ClickerScene firstScene = new ClickerScene(assets1, backgrounds[0]);
            ClickerScene secondScene = new ClickerScene(assets2, backgrounds[1]);
            
            firstScene.show();
            yield return new WaitForSeconds(3f);
            
            firstScene.hide();
            secondScene.show();
            yield return new WaitForSeconds(3f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
    }
}