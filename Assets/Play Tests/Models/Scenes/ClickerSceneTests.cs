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
        public IEnumerator ShowsBackground()
        {
            ClickerScene currentScene = new ClickerScene(assets, background);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
            Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

            Assert.AreEqual(background, currentBackground);
        }
        
        [UnityTest]
        public IEnumerator ShowsAssets()
        {
            ClickerScene currentScene = new ClickerScene(assets);

            currentScene.show();
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            Transform asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(assets[0].getPrefab().name + "(Clone)", asset.name);

            Vector3 position = asset.position;
            Assert.AreEqual(Math.Floor(assets[0].getPosition().x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(assets[0].getPosition().y), Math.Floor(position.y));
        }
    }
}

