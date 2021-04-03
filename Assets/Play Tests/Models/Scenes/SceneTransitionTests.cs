using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections.Generic;

namespace Visual
{
    //Comment out this line for local testing purposes, otherwise Unity will not run these tests.
    [Ignore("These tests are purely cosmetic, some requiring mouse inputs.")]
    public class SceneTransitionTests
    {
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };
        
        private List<Asset> assets1 = new List<Asset> 
        {
            new Asset("CA [Kitten]",
                new Vector3(130, 92), 
                new PaCElement(null))
        };

        private Asset kitten = new Asset("CP [Kitten]",
            new Vector3(0, 0),
            new Character("Kitten", 
                "While I may be soft and cute, I'm also lost and scared. " +
                "I don't know where I am, what's going on, or how I got here."));

        private Asset cat = new Asset("CP [Cat]",
            new Vector3(50, 0),
            new Character("Cat",
                "That's very unfortunate, small kitten. I wish I could help you out there. " + 
                "However, this is only a test and soon we will vanish the void."));
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator CutsceneToCutscene()
        {
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");
            DialogueViewer dlViewer = eventSystem.GetComponent<DialogueViewer>();
            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            
            Cutscene secondScene = new Cutscene((cat, kitten), backgrounds[1]);
            Cutscene firstScene = new Cutscene((kitten, cat), backgrounds[0], secondScene);
            
            sNavi.setCurrentScene(firstScene);
            
            firstScene.show();
            yield return new WaitUntil(() => dlViewer.getNavButton().getState().isClicked());
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            yield return new WaitForSeconds(1f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator CutsceneToPaC()
        {
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");
            DialogueViewer dlViewer = eventSystem.GetComponent<DialogueViewer>();
            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            
            PointandClick secondScene = new PointandClick(assets1, backgrounds[0]);
            Cutscene firstScene = new Cutscene((kitten, cat), backgrounds[1], secondScene);
            
            sNavi.setCurrentScene(firstScene);
            
            firstScene.show();
            yield return new WaitUntil(() => dlViewer.getNavButton().getState().isClicked());
            yield return new WaitForSeconds(2f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator PaCToCutscene()
        {
            DialogueViewer dlViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<DialogueViewer>();
            
            Cutscene secondScene = new Cutscene((kitten, null), backgrounds[1]);
            PointandClick firstScene = new PointandClick(assets1, backgrounds[0]);
            
            firstScene.show();
            yield return new WaitForSeconds(2f);
            
            firstScene.hide();
            secondScene.show();
            yield return new WaitUntil(() => !dlViewer.getIsTyping());
            yield return new WaitForSeconds(1f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator PaCToPaC()
        {
            List<Asset> assets2 = new List<Asset> 
            {
                new Asset("CA [Kitten]",
                    new Vector3(270, 132), 
                    new PaCElement(null))
            };

            PointandClick secondScene = new PointandClick(assets2, backgrounds[1]);
            PointandClick firstScene = new PointandClick(assets1, backgrounds[0]);
            
            firstScene.show();
            yield return new WaitForSeconds(2f);
            
            firstScene.hide();
            secondScene.show();
            yield return new WaitForSeconds(2f);
            
            Assert.Inconclusive("Does the transition look smooth?");
        }
    }
}