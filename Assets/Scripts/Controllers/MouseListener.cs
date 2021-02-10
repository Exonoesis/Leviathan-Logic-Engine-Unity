using UnityEngine;

public class MouseListener : MonoBehaviour
{
    private AssetViewer aViewer;

    void Start()
    {
        aViewer = AssetViewer.Instance;
    }
    
    void OnMouseOver()
    {
        aViewer.Darken(gameObject);
        
        if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
        {
            aViewer.handleClickedPrefab(transform.parent.gameObject);
        }
    }

    void OnMouseExit()
    {
        aViewer.Lighten(gameObject);
    }
}