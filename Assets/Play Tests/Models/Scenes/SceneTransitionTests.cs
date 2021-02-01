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
        private string speaker1 = "Eevee";
        private string dialogue1 = "While I may be soft and cute, I'm also lost and scared. " +
                                   "I don't know where I am, what's going on, or how I got here.";
        
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };
        
        private List<Asset> assets1 = new List<Asset> 
        {
            new Asset("CA [Eevee]",
                new Vector3(130, 92), 
                null)
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
            string dialogue2 = "That's very unfortunate, Eevee. I wish I could help you out there. "
                               + "However this is only a test and soon we will vanish.";
            
            Cutscene scene1 = new Cutscene(speaker1, dialogue1, backgrounds[0]);
            Cutscene scene2 = new Cutscene(speaker2, dialogue2, backgrounds[1]);

            scene1.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            scene1.hide();
            scene2.show();
            
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator CutsceneClicker()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            Cutscene scene1 = new Cutscene(speaker1, dialogue1, backgrounds[1]);
            ClickerScene scene2 = new ClickerScene(assets1, backgrounds[0]);
            
            scene1.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            scene1.hide();
            scene2.show();
            yield return new WaitForSeconds(3f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerCutscene()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();

            ClickerScene scene1 = new ClickerScene(assets1, backgrounds[0]);
            Cutscene scene2 = new Cutscene(speaker1, dialogue1, backgrounds[1]);
            
            scene1.show();
            yield return new WaitForSeconds(3f);
            
            scene1.hide();
            scene2.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerClicker()
        {
            List<Asset> assets2 = new List<Asset> 
            {
                new Asset("CA [Eevee]",
                    new Vector3(230, 192), 
                    null)
            };

            ClickerScene scene1 = new ClickerScene(assets1, backgrounds[0]);
            ClickerScene scene2 = new ClickerScene(assets2, backgrounds[1]);
            
            scene1.show();
            yield return new WaitForSeconds(3f);
            
            scene1.hide();
            scene2.show();
            yield return new WaitForSeconds(3f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
    }
}