using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    public class ConditionalsTests
    {
        private List<Asset> assets = new List<Asset> 
        {
            new Asset("CA [Kitten]",
                new Vector3(130, 92), 
                new PaCElement(null))
        };
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator HasBeenClickedConditionalPass()
        {
            Asset kitten = new Asset("CP [Kitten]",
                new Vector3(0, 0),
                new Character("Kitten", "Meow"));
            
            Cutscene dummyScene = new Cutscene(
                (kitten, null), 
                Resources.Load<Texture>("Images/BG/Trees"));
            
            assets[0].getState().setNextScene(dummyScene);
            
            PointandClick currentScene = new PointandClick(assets);

            currentScene.show();
            SceneNavigator.Instance.setCurrentScene(currentScene);
            
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject);
            asset.getState().Click(asset);
                
            Assert.IsTrue(asset.getState().isClicked());

            HasBeenClicked clickedConditional = new HasBeenClicked(asset);
            
            Assert.IsTrue(clickedConditional.isMet());
        }
        
        [UnityTest]
        public IEnumerator HasBeenClickedConditionalFail()
        {
            PointandClick currentScene = new PointandClick(assets);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getSceneAssetFrom(aPanel.transform.GetChild(0).gameObject);
            
            Assert.IsFalse(asset.getState().isClicked());
            
            HasBeenClicked clickedConditional = new HasBeenClicked(asset);

            Assert.IsFalse(clickedConditional.isMet());
        }
    }
}