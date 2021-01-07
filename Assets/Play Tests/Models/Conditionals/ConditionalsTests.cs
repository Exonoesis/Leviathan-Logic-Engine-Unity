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
        public IEnumerator HasBeenClickedConditionalPass()
        {
            ClickerScene assetOnlyScene = new ClickerScene(assets);

            assetOnlyScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getAsset(aPanel.transform.GetChild(0).gameObject);
            asset.incrementClickedNum();
            
            Assert.AreEqual(1, asset.getClickedNum());

            HasBeenClicked clickedConditional = new HasBeenClicked(asset);
            
            Assert.IsTrue(clickedConditional.isMet());
        }
        
        [UnityTest]
        public IEnumerator HasBeenClickedConditionalFail()
        {
            ClickerScene assetOnlyScene = new ClickerScene(assets);

            assetOnlyScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset asset = aViewer.getAsset(aPanel.transform.GetChild(0).gameObject);
            
            Assert.AreEqual(0, asset.getClickedNum());
            
            HasBeenClicked clickedConditional = new HasBeenClicked(asset);

            Assert.IsFalse(clickedConditional.isMet());
        }
    }
}