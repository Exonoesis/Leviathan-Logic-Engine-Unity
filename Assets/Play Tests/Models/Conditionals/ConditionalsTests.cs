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
        private List<ClickerSceneAsset> assets = new List<ClickerSceneAsset> 
        {
            new ClickerSceneAsset("CA [Eevee]",
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
            
            ClickerSceneAsset clickerSceneAsset = (ClickerSceneAsset) aViewer.getAsset(aPanel.transform.GetChild(0).gameObject);
            clickerSceneAsset.incrementClickedNum();
            
            Assert.AreEqual(1, clickerSceneAsset.getClickedNum());

            HasBeenClicked clickedConditional = new HasBeenClicked(clickerSceneAsset);
            
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
            
            ClickerSceneAsset clickerSceneAsset = (ClickerSceneAsset) aViewer.getAsset(aPanel.transform.GetChild(0).gameObject);
            
            Assert.AreEqual(0, clickerSceneAsset.getClickedNum());
            
            HasBeenClicked clickedConditional = new HasBeenClicked(clickerSceneAsset);

            Assert.IsFalse(clickedConditional.isMet());
        }
    }
}