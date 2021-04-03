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
    public class PaCSceneTests
    {
        private Texture background = Resources.Load<Texture>("Images/BG/Stairs");
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
        public IEnumerator ShowsBackground()
        {
            PointandClick currentScene = new PointandClick(assets, background);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            Texture currentBackground = GameObject
                .FindWithTag("BGPanelStatic")
                .GetComponent<RawImage>()
                .texture;

            Assert.AreEqual(background, currentBackground);
        }
        
        [UnityTest]
        public IEnumerator ShowsAssets()
        {
            PointandClick currentScene = new PointandClick(assets);
            
            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            Transform asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(assets[0].getPrefab().name, asset.name);

            Vector3 scenePosition = asset.position;
            Vector3 desiredPosition = assets[0].getPosition();
            
            Assert.AreEqual(Math.Floor(desiredPosition.x), Math.Floor(scenePosition.x));
            Assert.AreEqual(Math.Floor(desiredPosition.y), Math.Floor(scenePosition.y));
        }
        
        [UnityTest]
        public IEnumerator RemovesAssetsFromScene()
        {
            PointandClick currentScene = new PointandClick(assets);
            
            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(assets.Count, numAssets);

            currentScene.hide();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }
    }
}

