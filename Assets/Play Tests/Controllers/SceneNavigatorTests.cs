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
        private Asset passKitten = new Asset("CP [Kitten]",
            new Vector3(0, 0),
            new Character("Kitten", "How did I get here?"));
        
        private List<Texture> backgrounds = new List<Texture>
        {
            Resources.Load<Texture>("Images/BG/Trees"),
            Resources.Load<Texture>("Images/BG/Stairs")
        };

        private string assetName = "CA [Kitten]";
        private Vector3 assetPosition = new Vector3(130, 92);
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator NoConditionals()
        {
            Cutscene nextScene = new Cutscene((passKitten, null), backgrounds[0]);
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

            Assert.AreEqual(nextScene, sNavi.getCurrentScene());
        }
        
        [UnityTest]
        public IEnumerator OnePass()
        {
            Cutscene nextScene = new Cutscene((passKitten, null), backgrounds[0]);
            Asset asset = new Asset(assetName, assetPosition, new PaCElement(nextScene));
            PointandClick currentScene = new PointandClick(new List<Asset>{asset}, backgrounds[1]);

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

            Assert.AreEqual(nextScene, sNavi.getCurrentScene());
        }
        
        [UnityTest]
        public IEnumerator OneFail()
        {
            Asset failKitten = new Asset("CP [Kitten]",
                new Vector3(0, 0),
                new Character("Kitten", "We're missing an important item."));
            
            Cutscene nextScene = new Cutscene((passKitten, null), backgrounds[0]);
            Cutscene errorScene = new Cutscene((failKitten, null), backgrounds[1]);
            Asset asset = new Asset(assetName, assetPosition, new PaCElement(nextScene));
            PointandClick currentScene = new PointandClick(new List<Asset>{asset}, backgrounds[1]);

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

            Assert.AreEqual(errorScene, sNavi.getCurrentScene());

            Asset navButton = dlViewer.getNavButton();
            navButton.getState().Click(navButton);
            
            Assert.AreEqual(currentScene, sNavi.getCurrentScene());
            
            yield return new WaitForSeconds(3f);
        }
        
        [UnityTest]
        public IEnumerator OnePassOneFail()
        {
            Asset e1kitten = new Asset("CP [Kitten]",
                new Vector3(0, 0),
                new Character("Kitten", "I should not be here."));

            Asset e2kitten = new Asset("CP [Kitten]",
                new Vector3(50, 0),
                new Character("Kitten", "Oops!"));
            
            List<Cutscene> errorScenes = new List<Cutscene>
            {
                new Cutscene((e1kitten, null), backgrounds[1]),
                new Cutscene((e2kitten, null), backgrounds[1])
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