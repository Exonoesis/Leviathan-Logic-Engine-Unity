using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTesting : MonoBehaviour
{
    BackgroundViewer behavior;

    public Texture tex;
    public RawImage activeImage;

    void Start()
    {
        behavior = BackgroundViewer.instance;
    }

    void Update()
    { 
        if (Input.GetKey(KeyCode.S))
        {
            behavior.Transition(tex);
        }
    }
}
