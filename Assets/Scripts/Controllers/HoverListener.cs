using UnityEngine;

public class HoverListener : MonoBehaviour
{
    private PolygonCollider2D objCollider;
    private AssetViewer aViewer;

    void Start()
    {
        objCollider = gameObject.GetComponent<PolygonCollider2D> ();
        aViewer = AssetViewer.Instance;
    }

    void Update()
    {
        Vector3 mPos = Input.mousePosition;

        if (objCollider.bounds.Contains(mPos))
        {
            aViewer.Darken(gameObject);

            if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                aViewer.handleClickedPrefab(transform.parent.gameObject);
            }
        }
        else
        {
            aViewer.Lighten(gameObject);
        }
    }
}