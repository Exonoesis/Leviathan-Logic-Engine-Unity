using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Visual
{
    public class AssetViewerTests
    {
        private Asset desiredAsset = new Asset(
            "CA [Eevee]", 
            new Vector3(130, 92), 
            null);
        
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator PlacesAsset()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            var asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(desiredAsset.getPrefab().name + "(Clone)", asset.name);

            Vector3 position = asset.position;
            Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(position.y));
        }
        
        [UnityTest]
        public IEnumerator AssetDims()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject asset = GameObject.FindWithTag("Eevee");
            Image assetImage = asset.GetComponent<Image>();
            
            Assert.AreEqual(Color.white, assetImage.color);

            aViewer.Darken(asset);

            Assert.AreEqual(Color.grey, assetImage.color);
        }
        
        [UnityTest]
        public IEnumerator AssetLightensAfterDimming()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();

            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject asset = GameObject.FindWithTag("Eevee");
            Image assetImage = asset.GetComponent<Image>();

            aViewer.Darken(asset);

            Assert.AreEqual(Color.grey, assetImage.color);

            aViewer.Lighten(asset);

            Assert.AreEqual(Color.white, assetImage.color);
        }
        
        [UnityTest]
        public IEnumerator RemovesAssetFromScene()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();

            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(1, numAssets);

            aViewer.clearAssets();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }
    }
}