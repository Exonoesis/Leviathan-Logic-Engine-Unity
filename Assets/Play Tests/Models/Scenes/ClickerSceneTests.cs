using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Visual
{
    public class ClickerSceneTests
    {
        private Texture background = Resources.Load<Texture>("Images/BG/Stairs");
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
        public IEnumerator ShowsBackground()
        {
            PointandClick currentScene = new PointandClick(assets, background);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
            Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

            Assert.AreEqual(background, currentBackground);
        }
        
        [UnityTest]
        public IEnumerator ShowsAssets()
        {
            PointandClick assetOnlyScene = new PointandClick(assets);
            
            assetOnlyScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            Transform asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(assets[0].getPrefab().name, asset.name);

            Vector3 position = asset.position;
            Assert.AreEqual(Math.Floor(assets[0].getPosition().x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(assets[0].getPosition().y), Math.Floor(position.y));
        }
        
        [UnityTest]
        public IEnumerator RemovesAssetsFromScene()
        {
            PointandClick assetOnlyScene = new PointandClick(assets);
            
            assetOnlyScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(assets.Count, numAssets);

            assetOnlyScene.hide();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }
    }
}

