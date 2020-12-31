using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Visual
{
    public class AssetViewerTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator PlacesAsset()
        {
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");
            AssetViewer aViewer = eventSystem.GetComponent<AssetViewer>();
            
            Vector3 desiredAssetPosition = new Vector3(130, 92);
            Asset desiredAsset = new Asset("CA [Eevee]", desiredAssetPosition, null);
            
            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            var asset = aPanel.transform.GetChild(0);

            Assert.AreEqual(desiredAsset.getPrefab().name + "(Clone)", asset.name);

            Vector3 position = asset.position;
            Assert.AreEqual(Math.Floor(desiredAsset.getPosition().x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(desiredAsset.getPosition().y), Math.Floor(position.y));
        }
    }
}