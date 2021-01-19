using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections.Generic;

namespace Interactive
{
    public class ConditionalCheckerTests
    {
        private string speaker = "Eevee";
        private string passDialogue = "How did I get here?";
        private string failDialogue = "We're missing an important item.";
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };

        private string assetName = "CA [Eevee]";
        private Vector3 assetPosition = new Vector3(130, 92);
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        /*
        Due to some sort of issue with loading scenes a teardown method is required in order to properly reset
        the scene. Without it, backgrounds are not visually appearing in certain tests.
        UnloadScene() was chosen - despite being obsolete - because, and I quote, 
              
        "It is not possible to UnloadSceneAsync if there are no scenes to load. 
        For example, a project that has a single scene cannot use this static member."
        
        [TearDown]
        public void TearDown()
        {
            SceneManager.UnloadScene(0);
        }
        */
        [UnityTest]
        public IEnumerator NoConditionals()
        {
            Cutscene nextScene = new Cutscene(speaker, passDialogue, backgrounds[0]);

            ClickerSceneAsset clickerSceneAsset = new ClickerSceneAsset(assetName, assetPosition, nextScene);
            
            ClickerScene currentScene = new ClickerScene(new List<ClickerSceneAsset>{clickerSceneAsset}, backgrounds[1]
                );

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            ConditionalChecker cChecker = eventSystem.GetComponent<ConditionalChecker>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            cChecker.setCurrentScene(currentScene);

            yield return new WaitForSeconds(1f);

            aViewer.handleClickedPrefab(aPanel.transform.GetChild(0).gameObject);

            yield return new WaitForSeconds(3f);

            Assert.AreEqual(cChecker.getCurrentScene(), nextScene);
        }
        
        [UnityTest]
        public IEnumerator OnePass()
        {
            Cutscene nextScene = new Cutscene(speaker, passDialogue, backgrounds[0]);

            ClickerSceneAsset clickerSceneAsset = new ClickerSceneAsset(assetName, assetPosition, nextScene);

            ClickerScene currentScene = new ClickerScene(new List<ClickerSceneAsset>{clickerSceneAsset}, 
                backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            ConditionalChecker cChecker = eventSystem.GetComponent<ConditionalChecker>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            cChecker.setCurrentScene(currentScene);
            
            cChecker.addConditions(clickerSceneAsset, 
                new List<Conditional>
                {
                    new HasBeenClicked(clickerSceneAsset)
                });

            yield return new WaitForSeconds(1f);

            aViewer.handleClickedPrefab(aPanel.transform.GetChild(0).gameObject);

            yield return new WaitForSeconds(3f);

            Assert.AreEqual(cChecker.getCurrentScene(), nextScene);
        }
        
        [UnityTest]
        public IEnumerator OneFail()
        {
            Cutscene nextScene = new Cutscene(speaker, passDialogue, backgrounds[0]);
            Cutscene errorScene = new Cutscene(speaker, failDialogue, backgrounds[1]);

            ClickerSceneAsset clickerSceneAsset = new ClickerSceneAsset(assetName, assetPosition, nextScene);
            
            ClickerScene currentScene = new ClickerScene(new List<ClickerSceneAsset>{clickerSceneAsset}, 
                backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            ConditionalChecker cChecker = eventSystem.GetComponent<ConditionalChecker>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            cChecker.setCurrentScene(currentScene);

            HasBeenClicked clickedCondition = new HasBeenClicked(clickerSceneAsset);
            List<Conditional> conditionals = new List<Conditional>{clickedCondition};

            cChecker.addConditions(clickerSceneAsset, conditionals);
            cChecker.addErrorScene(clickerSceneAsset, clickedCondition, errorScene);

            yield return new WaitForSeconds(1f);
            
            cChecker.changeSceneIfSatisfied(aViewer.getAsset(aPanel.transform.GetChild(0).gameObject));

            yield return new WaitForSeconds(3f);

            Assert.AreEqual(cChecker.getCurrentScene(), errorScene);
        }
        
        [UnityTest]
        public IEnumerator OnePassOneFail()
        {
            List<Scene> errorScenes = new List<Scene>
            {
                new Cutscene(speaker, "I should not be here", backgrounds[1]),
                new Cutscene(speaker, "Oops", backgrounds[1])
            };
            
            ClickerSceneAsset passingClickerSceneAsset = new ClickerSceneAsset(assetName, assetPosition, null);
            ClickerSceneAsset placeholderClickerSceneAsset = new ClickerSceneAsset(assetName, new Vector3(275,147), null);

            List<ClickerSceneAsset> assets = new List<ClickerSceneAsset>()
            {
                passingClickerSceneAsset,
                placeholderClickerSceneAsset
            };
            
            ClickerScene currentScene = new ClickerScene(assets, backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            ConditionalChecker cChecker = eventSystem.GetComponent<ConditionalChecker>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            cChecker.setCurrentScene(currentScene);

            HasBeenClicked passingCondition = new HasBeenClicked(passingClickerSceneAsset);
            HasBeenClicked failingCondition = new HasBeenClicked(placeholderClickerSceneAsset);
            List<Conditional> conditionals = new List<Conditional>
            {
                passingCondition,
                failingCondition
            };

            cChecker.addConditions(passingClickerSceneAsset, conditionals);
            
            cChecker.addErrorScene(passingClickerSceneAsset, passingCondition, errorScenes[0]);
            cChecker.addErrorScene(passingClickerSceneAsset, failingCondition, errorScenes[1]);

            yield return new WaitForSeconds(1f);
            
            aViewer.handleClickedPrefab(aPanel.transform.GetChild(0).gameObject);

            yield return new WaitForSeconds(3f);

            Assert.AreEqual(cChecker.getCurrentScene(), errorScenes[1]);
        }
    }
}