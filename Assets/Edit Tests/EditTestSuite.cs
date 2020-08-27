using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

[TestFixture]
public class EditTestSuite
{

    [SetUp]
    public void Setup()
    {

    }

    [TearDown]
    public void Teardown()
    {

    }

    [Test]
    public void testAssetDimsOnHover()
    {
        GameObject eevee = GameObject.FindWithTag("Eevee");
        Image eeveeImage = eevee.GetComponent<Image>();
        HoverListener eeveeHoverListener = eevee.GetComponent<HoverListener>();

        Assert.AreEqual(eeveeImage.color, Color.white);

        eeveeHoverListener.Darken(eeveeImage);

        Assert.AreEqual(eeveeImage.color, Color.grey);
    }

    [Test]
    public void testAssetLightensOnHoverExit()
    {
        GameObject eevee = GameObject.FindWithTag("Eevee");
        Image eeveeImage = eevee.GetComponent<Image>();
        HoverListener eeveeHoverListener = eevee.GetComponent<HoverListener>();

        eeveeHoverListener.Darken(eeveeImage);

        Assert.AreEqual(eeveeImage.color, Color.grey);

        eeveeHoverListener.Lighten(eeveeImage);

        Assert.AreEqual(eeveeImage.color, Color.white);
    }
}
