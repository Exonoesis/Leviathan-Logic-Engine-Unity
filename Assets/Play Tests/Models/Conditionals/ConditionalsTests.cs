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
            new Asset("CA [Cat]",
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
            Cutscene dummyScene = new Cutscene("Duck",
                "Quack!", 
                Resources.Load<Texture>("Images/BG/Trees"));
            
            assets[0].getState().setNextScene(dummyScene);
            
            PointandClick assetOnlyScene = new PointandClick(assets);

            assetOnlyScene.show();
            SceneNavigator.Instance.setCurrentScene(assetOnlyScene);
            
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getAssetFrom(aPanel.transform.GetChild(0).gameObject);
            asset.getState().Click(asset);
                
            Assert.IsTrue(asset.getState().isClicked());

            HasBeenClicked clickedConditional = new HasBeenClicked(asset);
            
            Assert.IsTrue(clickedConditional.isMet());
        }
        
        [UnityTest]
        public IEnumerator HasBeenClickedConditionalFail()
        {
            PointandClick assetOnlyScene = new PointandClick(assets);

            assetOnlyScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getAssetFrom(aPanel.transform.GetChild(0).gameObject);
            
            Assert.IsFalse(asset.getState().isClicked());
            
            HasBeenClicked clickedConditional = new HasBeenClicked(asset);

            Assert.IsFalse(clickedConditional.isMet());
        }
    }
}