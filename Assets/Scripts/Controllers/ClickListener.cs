using UnityEngine;

public class ClickListener : MonoBehaviour
{
    private PolygonCollider2D objCollider;
    private AssetViewer aViewer;
    private DialogueViewer dlViewer;
    //private bool isEnabled;

    void Start()
    {
        objCollider = gameObject.GetComponent<PolygonCollider2D> ();
        aViewer = AssetViewer.Instance;
        dlViewer = DialogueViewer.Instance;
    }
    
    void Update()
    {
        Vector3 mPos = Input.mousePosition;

        if (dlViewer.getIsTyping())
        {
            //isEnabled = false;
            //Call changeImage() in AssetViewer?
        }
        else
        {
            //isEnabled = true;
            //Call changeImage() in AssetViewer?
            
            if (objCollider.bounds.Contains(mPos) &&
                Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                aViewer.handleClickedPrefab(transform.parent.gameObject);
            }
        }
    }
}
