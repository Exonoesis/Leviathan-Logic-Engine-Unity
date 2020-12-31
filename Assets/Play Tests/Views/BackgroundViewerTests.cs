using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Visual
{
    public class BackgroundViewerTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }
        
        [UnityTest]
        public IEnumerator ShowsTransition()
        {
            GameObject eventSystem = GameObject.FindWithTag("EventSystem");
            BackgroundViewer bgViewer = eventSystem.GetComponent<BackgroundViewer>();
            
            Texture desiredBackground = Resources.Load<Texture>("Images/BG/Stairs");

            bgViewer.Transition(desiredBackground);
            yield return new WaitForSeconds(1f);

            GameObject staticPanel = GameObject.FindWithTag("BGPanelStatic");
            Texture currentBackground = staticPanel.GetComponent<RawImage>().texture;

            Assert.AreEqual(desiredBackground, currentBackground);
        }
    }
}