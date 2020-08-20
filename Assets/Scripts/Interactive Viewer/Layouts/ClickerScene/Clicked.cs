using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
     
public class Clicked : MonoBehaviour
{
    public RectTransform rectTransform;
    //public MaskableGraphic buttonIcon;
    public UnityEvent onClicked;

    private Rect rectangle;
    
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform> ();
        //buttonIcon = gameObject.GetComponent<MaskableGraphic> ();   
    }

    void Update()
    {
        rectangle = rectTransform.rect;
        Vector3 mPos = Input.mousePosition;

        if (mPos.x > rectTransform.position.x - (rectangle.width) 
        && mPos.x < rectTransform.position.x + (rectangle.width)
        && mPos.y > rectTransform.position.y - (rectangle.height)
        && mPos.y < rectTransform.position.y + (rectangle.height)
        && Input.GetMouseButtonUp (0)) 
        {
            onClicked.Invoke(); 
        } 
    }
}