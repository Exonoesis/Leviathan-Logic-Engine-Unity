﻿using UnityEngine;

public class CutsceneTesting : MonoBehaviour
{
    public string sceneName;
    public Texture background;
    public string speaker;
    public string dialogue; //Should be an array for additive text?
    public string[] next;

    Cutscene test;

    void Start()
    {
        test = new Cutscene();

        test.sceneName = name;
        test.background = background;
        test.speaker = speaker;
        test.dialogue = dialogue;
        test.next = next;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            test.show();
        }
    }
}
