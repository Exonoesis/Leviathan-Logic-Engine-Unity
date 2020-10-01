using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoverListener : MonoBehaviour
{
    private PolygonCollider2D objCollider;
    private Image spriteImage;
    private AssetViewer aViewer;

    void Start()
    {
        objCollider = gameObject.GetComponent<PolygonCollider2D> ();
        spriteImage = gameObject.GetComponent<Image> ();
        aViewer = AssetViewer.Instance;
    }

    void Update()
    {
        Vector3 mPos = Input.mousePosition;

        if (objCollider.bounds.Contains(mPos))
        {
            Darken(spriteImage);

            if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                print(name);
                //aViewer.handleClickedPrefab();
            }
        }
        else
        {
            Lighten(spriteImage);
        }
    }

    public void Darken(Image image)
    {
        image.color = Color.grey;
    }

    public void Lighten(Image image)
    {
        image.color = Color.white;
    }
}