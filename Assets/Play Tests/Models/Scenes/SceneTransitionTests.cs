using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Visual
{
    public class SceneTransitionTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(0);
        }

        [UnityTest]
        public IEnumerator CutsceneCutscene()
        {
            yield return new WaitForSeconds(1f);
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator CutsceneClicker()
        {
            yield return new WaitForSeconds(1f);
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerCutscene()
        {
            yield return new WaitForSeconds(1f);
            Assert.Inconclusive("Does the transition look smooth?");
        }
        
        [UnityTest]
        public IEnumerator ClickerClicker()
        {
            yield return new WaitForSeconds(1f);
            Assert.Inconclusive("Does the transition look smooth?");
        }
    }
}