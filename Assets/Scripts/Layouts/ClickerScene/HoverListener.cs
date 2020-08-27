using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoverListener : MonoBehaviour
{
    public UnityEvent methodReference;

    private PolygonCollider2D objCollider;
    private Image spriteImage;

    void Start()
    {
        objCollider = gameObject.GetComponent<PolygonCollider2D> ();
        spriteImage = gameObject.GetComponent<Image> ();   
    }

    void Update()
    {
        Vector3 mPos = Input.mousePosition;

        if (objCollider.bounds.Contains(mPos))
        {
            Darken(spriteImage);

            if (Input.GetMouseButtonUp(MouseCodes.PrimaryButton))
            {
                //methodReference.Invoke();
                print("Method Call");
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