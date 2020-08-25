using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoverListener : MonoBehaviour
{
    public UnityEvent methodReference;

    private Color normalColor = Color.white;
    private Color hoverColor = Color.grey;

    private PolygonCollider2D objCollider;
    private MaskableGraphic image;

    void Start()
    {
        objCollider = gameObject.GetComponent<PolygonCollider2D> ();
        image = gameObject.GetComponent<MaskableGraphic> ();   
    }

    void Update()
    {
        Vector3 mPos = Input.mousePosition;

        if (objCollider.bounds.Contains(mPos))
        {
            image.color = hoverColor;

            if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                //methodReference.Invoke();
                print("Method Call");
            }
        }
        else
        {
            image.color = normalColor;
        }
    }
}