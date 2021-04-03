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
        private Asset _desiredAsset = new Asset(
            "CA [Kitten]", 
            new Vector3(130, 92),
            new PaCElement(null));
        
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
            
            aViewer.placeInScene(_desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            var asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(_desiredAsset.getPrefab().name, asset.name);

            Vector3 position = asset.position;
            Vector3 desiredPosition = _desiredAsset.getPosition();
            
            Assert.AreEqual(Math.Floor(desiredPosition.x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(desiredPosition.y), Math.Floor(position.y));
        }
        
        [UnityTest]
        public IEnumerator AssetDims()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            aViewer.placeInScene(_desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject asset = GameObject.FindWithTag("Kitten");
            Image assetImage = asset.GetComponent<Image>();
            
            Assert.AreEqual(Color.white, assetImage.color);

            aViewer.Darken(asset);

            Assert.AreEqual(Color.grey, assetImage.color);
        }
        
        [UnityTest]
        public IEnumerator AssetLightens()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();

            aViewer.placeInScene(_desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject asset = GameObject.FindWithTag("Kitten");
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

            aViewer.placeInScene(_desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(1, numAssets);

            aViewer.clearSceneAssets();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }
    }
}