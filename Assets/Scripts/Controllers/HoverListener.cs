using UnityEngine;
using UnityEngine.UI;

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
            aViewer.Darken(spriteImage);

            if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                aViewer.handleClickedPrefab(transform.parent.gameObject);
            }
        }
        else
        {
            aViewer.Lighten(spriteImage);
        }
    }
}