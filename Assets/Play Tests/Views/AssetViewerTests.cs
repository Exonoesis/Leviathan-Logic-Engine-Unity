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
    public class AssetViewerTests
    {
        private Asset cat = new Asset("CP [Cat]",
            new Vector3(0,0),
            new Character("Cat", "Meow"));
        
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
        public IEnumerator PlacesAsset()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset desiredAsset = assets[0];
            
            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            var asset = GameObject
                .FindWithTag("AssetsPanel")
                .transform
                .GetChild(0);

            Assert.AreEqual(desiredAsset.getPrefab().name, asset.name);

            Vector3 position = asset.position;
            Vector3 desiredPosition = desiredAsset.getPosition();
            
            Assert.AreEqual(Math.Floor(desiredPosition.x), Math.Floor(position.x));
            Assert.AreEqual(Math.Floor(desiredPosition.y), Math.Floor(position.y));
        }
        
        [UnityTest]
        public IEnumerator AssetDims()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Asset desiredAsset = assets[0];
            
            aViewer.placeInScene(desiredAsset);
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

            Asset desiredAsset = assets[0];
            
            aViewer.placeInScene(desiredAsset);
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

            Asset desiredAsset = assets[0];
            
            aViewer.placeInScene(desiredAsset);
            yield return new WaitForSeconds(1f);

            GameObject aPanel = GameObject.FindWithTag("AssetsPanel");
            int numAssets = aPanel.transform.childCount;

            Assert.AreEqual(1, numAssets);

            aViewer.clearSceneAssets();
            yield return new WaitForSeconds(1f);

            numAssets = aPanel.transform.childCount;

            Assert.AreEqual(0, numAssets);
        }

        [UnityTest]
        public IEnumerator MoveToMoveTowards()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Scene[] scenes = {new PointandClick(assets), new Cutscene((cat, null))};
            
            foreach (Scene scene in scenes)
            {
                scene.show();
                yield return new WaitForSeconds(1f);
                
                var prefab = GameObject
                    .FindWithTag("AssetsPanel")
                    .transform
                    .GetChild(0);

                Vector2 origPos = prefab.transform.position;
                Vector2 targetPos = new Vector2(300, 25);
                
                aViewer.MoveTo(prefab.gameObject, targetPos,5f, MovementTypes.Smooth);
                yield return new WaitUntil(() => !aViewer.getIsMoving());

                Vector2 newPos = prefab.transform.position;
                Assert.AreNotEqual(origPos, newPos);

                int prefabDistance = Mathf.RoundToInt(Vector2.Distance(newPos, targetPos));
                Assert.AreEqual(0, prefabDistance);
                
                Asset asset = aViewer.getSceneAssetFrom(prefab.gameObject);
                asset.setPosition(origPos);
                
                scene.hide();
            }
        }
        
        [UnityTest]
        public IEnumerator MoveToLerp()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Scene[] scenes = {new PointandClick(assets), new Cutscene((cat, null))};
            
            foreach (Scene scene in scenes)
            {
                scene.show();
                yield return new WaitForSeconds(1f);
                
                var prefab = GameObject
                    .FindWithTag("AssetsPanel")
                    .transform
                    .GetChild(0);
                
                Vector2 origPos = prefab.transform.position;
                Vector2 targetPos = new Vector2(300, 25);
                
                aViewer.MoveTo(prefab.gameObject, targetPos,5f, MovementTypes.FastStart);
                yield return new WaitUntil(() => !aViewer.getIsMoving());
                
                Vector3 newPos = prefab.transform.position;
                Assert.AreNotEqual(origPos, newPos);
        
                int prefabDistance = Mathf.RoundToInt(Vector2.Distance(newPos, targetPos));
                Assert.AreEqual(0, prefabDistance);
                
                Asset asset = aViewer.getSceneAssetFrom(prefab.gameObject);
                asset.setPosition(origPos);
                
                scene.hide();
            }
        }
        
        [UnityTest]
        public IEnumerator MoveToSmoothDamp()
        {
            AssetViewer aViewer = GameObject
                .FindWithTag("EventSystem")
                .GetComponent<AssetViewer>();
            
            Scene[] scenes = {new PointandClick(assets), new Cutscene((cat, null))};
            
            foreach (Scene scene in scenes)
            {
                scene.show();
                yield return new WaitForSeconds(1f);
                
                var prefab = GameObject
                    .FindWithTag("AssetsPanel")
                    .transform
                    .GetChild(0);
                
                Vector2 origPos = prefab.transform.position;
                Vector2 targetPos = new Vector3(300, 25);
                
                aViewer.MoveTo(prefab.gameObject, targetPos,5f, MovementTypes.FastMiddle);
                yield return new WaitUntil(() => !aViewer.getIsMoving());
                
                Vector2 newPos = prefab.transform.position;
                Assert.AreNotEqual(origPos, newPos);
        
                int prefabDistance = Mathf.RoundToInt(Vector2.Distance(newPos, targetPos));
                Assert.AreEqual(0, prefabDistance);
        
                Asset asset = aViewer.getSceneAssetFrom(prefab.gameObject);
                asset.setPosition(origPos);
                
                scene.hide();
            }
        }
        
    }
}