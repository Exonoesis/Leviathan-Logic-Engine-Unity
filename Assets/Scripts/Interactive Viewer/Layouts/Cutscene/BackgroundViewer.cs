using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundViewer : MonoBehaviour
{
    public static BackgroundViewer instance;

    private GameObject BGStatic;
    private GameObject BGDynamic;

    private bool isTransitioning = false;
    private float speed = .5f;

    private RawImage dynamicImage;
    private RawImage staticImage;

    public void Awake()
    {
        instance = this;
        speed *= Time.deltaTime;

        BGStatic = GameObject.FindWithTag("BGPanelStatic");
        BGDynamic = GameObject.FindWithTag("BGPanelDynamic");
        BGDynamic.SetActive(false);

        dynamicImage = BGDynamic.GetComponent<RawImage>();
        staticImage = BGStatic.GetComponent<RawImage>();
    }

    public void Update()
    {
        if (isTransitioning)
        {
            dynamicImage.color = ToAlpha(dynamicImage.color, 
                Mathf.MoveTowards(dynamicImage.color.a, 0f, speed));

            if (dynamicImage.color.a == 0)
            {
                isTransitioning = false;
                BGDynamic.SetActive(false);
            }
        }

        else if (dynamicImage.color.a < 1)
        {
            dynamicImage.color = ToAlpha(dynamicImage.color, 1f);
        }
    }

    public void Transition(Texture tex)
    {
        if (!isTransitioning)
        {
            dynamicImage.texture = staticImage.texture;
            BGDynamic.SetActive(true);

            staticImage.texture = tex;
            isTransitioning = true;
        }
    }

    public static Color ToAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
