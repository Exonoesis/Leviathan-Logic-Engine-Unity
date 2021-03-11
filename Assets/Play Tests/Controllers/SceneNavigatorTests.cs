using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections.Generic;

namespace Interactive
{
    public class SceneNavigatorTests
    {
        private string speaker = "Cat";
        private string passDialogue = "How did I get here?";
        private string failDialogue = "We're missing an important item.";
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };

        private string assetName = "CA [Cat]";
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

            Asset asset = new Asset(assetName, assetPosition, new PaCElement(nextScene));
            
            PointandClick currentScene = new PointandClick(new List<Asset>{asset}, backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            sNavi.setCurrentScene(currentScene);

            yield return new WaitForSeconds(1f);

            Asset sceneAsset = aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject);
            sceneAsset.getState().Click(sceneAsset);
            
            yield return new WaitForSeconds(3f);

            Assert.AreEqual(sNavi.getCurrentScene(), nextScene);
        }
        
        [UnityTest]
        public IEnumerator OnePass()
        {
            Cutscene nextScene = new Cutscene(speaker, passDialogue, backgrounds[0]);

            Asset asset = new Asset(assetName, assetPosition, new PaCElement(nextScene));

            PointandClick currentScene = new PointandClick(new List<Asset>{asset}, 
                backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();

            currentScene.show();
            sNavi.setCurrentScene(currentScene);
            
            sNavi.addConditions(asset, 
                new List<Conditional>
                {
                    new HasBeenClicked(asset)
                });

            yield return new WaitForSeconds(1f);

            Asset sceneAsset = aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject);
            sceneAsset.getState().Click(sceneAsset);
            
            yield return new WaitForSeconds(3f);

            Assert.AreEqual(sNavi.getCurrentScene(), nextScene);
        }
        
        [UnityTest]
        public IEnumerator OneFail()
        {
            Cutscene nextScene = new Cutscene(speaker, passDialogue, backgrounds[0]);
            Cutscene errorScene = new Cutscene(speaker, failDialogue, backgrounds[1]);

            Asset asset = new Asset(assetName, assetPosition, new PaCElement(nextScene));
            
            PointandClick currentScene = new PointandClick(new List<Asset>{asset}, 
                backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();
            DialogueViewer dlViewer = eventSystem.GetComponent<DialogueViewer>();

            currentScene.show();
            sNavi.setCurrentScene(currentScene);

            HasBeenClicked clickedCondition = new HasBeenClicked(asset);
            List<Conditional> conditionals = new List<Conditional>{clickedCondition};

            sNavi.addConditions(asset, conditionals);
            sNavi.addErrorScene(asset, clickedCondition, errorScene);

            yield return new WaitForSeconds(1f);
            
            sNavi.changeSceneIfSatisfied(aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject));

            yield return new WaitForSeconds(3f);

            Assert.AreEqual(sNavi.getCurrentScene(), errorScene);

            Asset navButton = dlViewer.getNavButton();
            navButton.getState().Click(navButton);
            
            Assert.AreEqual(currentScene, sNavi.getCurrentScene());
            
            yield return new WaitForSeconds(3f);
        }
        
        [UnityTest]
        public IEnumerator OnePassOneFail()
        {
            List<Cutscene> errorScenes = new List<Cutscene>
            {
                new Cutscene(speaker, "I should not be here", backgrounds[1]),
                new Cutscene(speaker, "Oops", backgrounds[1])
            };
            
            Asset passingAsset = new Asset(
                assetName, 
                assetPosition,
                new PaCElement(null));
            
            Asset placeholderAsset = new Asset(
                assetName, 
                new Vector3(275,147), 
                new PaCElement(null));

            List<Asset> assets = new List<Asset>()
            {
                passingAsset,
                placeholderAsset
            };
            
            PointandClick currentScene = new PointandClick(assets, backgrounds[1]);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");

            SceneNavigator sNavi = eventSystem.GetComponent<SceneNavigator>();
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();
            DialogueViewer dlViewer = eventSystem.GetComponent<DialogueViewer>();

            currentScene.show();
            sNavi.setCurrentScene(currentScene);

            HasBeenClicked passingCondition = new HasBeenClicked(passingAsset);
            HasBeenClicked failingCondition = new HasBeenClicked(placeholderAsset);
            List<Conditional> conditionals = new List<Conditional>
            {
                passingCondition,
                failingCondition
            };

            sNavi.addConditions(passingAsset, conditionals);
            
            sNavi.addErrorScene(passingAsset, passingCondition, errorScenes[0]);
            sNavi.addErrorScene(passingAsset, failingCondition, errorScenes[1]);

            yield return new WaitForSeconds(1f);
            
            Asset sceneAsset = aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject);
            sceneAsset.getState().Click(sceneAsset);
            
            yield return new WaitForSeconds(3f);

            Assert.AreEqual(errorScenes[1], sNavi.getCurrentScene());
            
            Asset navButton = dlViewer.getNavButton();
            navButton.getState().Click(navButton);
            
            Assert.AreEqual(currentScene, sNavi.getCurrentScene());
            
            yield return new WaitForSeconds(3f);
        }
    }
}