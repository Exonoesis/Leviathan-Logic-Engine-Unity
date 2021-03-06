using UnityEngine;

public class CoreAssetMouseListener : MonoBehaviour
{
    private AssetViewer aViewer;

    void Start()
    {
        aViewer = AssetViewer.Instance;
    }
    
    void OnMouseOver()
    {
        Asset selectedAsset = aViewer.getCoreAssetFrom(transform.parent.gameObject);
        
        State assetState = selectedAsset.getState();
        assetState.HoverEnter(selectedAsset);
        
        if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
        {
            assetState.Click(selectedAsset);
        }
    }

    void OnMouseExit()
    {
        Asset selectedAsset = aViewer.getCoreAssetFrom(transform.parent.gameObject);

        State assetState = selectedAsset.getState();
        assetState.HoverExit(selectedAsset);
    }
}