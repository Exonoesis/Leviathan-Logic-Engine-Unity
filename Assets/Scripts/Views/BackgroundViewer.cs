using UnityEngine;
using UnityEngine.UI;

public class BackgroundViewer : MonoBehaviour
{
    private static BackgroundViewer _instance;
    public static BackgroundViewer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BackgroundViewer>();
            }
            return _instance;
        }
    }

    private GameObject BGStatic;
    private GameObject BGDynamic;
    
    private RawImage staticImage;
    private RawImage dynamicImage;

    private bool isTransitioning;
    private float fadeSpeed = .5f;

    public void Awake()
    {
        fadeSpeed *= Time.deltaTime;

        BGStatic = GameObject.FindWithTag("BGPanelStatic");
        BGDynamic = GameObject.FindWithTag("BGPanelDynamic");
        
        dynamicImage = BGDynamic.GetComponent<RawImage>();
        staticImage = BGStatic.GetComponent<RawImage>();
        
        BGDynamic.SetActive(false);
    }

    public void Update()
    {
        if (isTransitioning)
        {
            dynamicImage.color = toAlpha(dynamicImage.color,
                Mathf.MoveTowards(
                    dynamicImage.color.a,
                    0f, 
                    fadeSpeed));

            if (dynamicImage.color.a <= 0)
            {
                isTransitioning = false;
                BGDynamic.SetActive(false);
            }
        }

        else if (dynamicImage.color.a < 1)
        {
            dynamicImage.color = toAlpha(dynamicImage.color, 1f);
        }
    }

    public void Transition(Texture texture)
    {
        if (!isTransitioning && texture != null)
        {
            dynamicImage.texture = staticImage.texture;
            BGDynamic.SetActive(true);

            staticImage.texture = texture;
            isTransitioning = true;
        }
    }

    public static Color toAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}