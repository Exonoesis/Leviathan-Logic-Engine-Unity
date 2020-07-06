using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTesting : MonoBehaviour
{
    public string name;
    public Texture background;
    public string speaker;
    public string dialogue; //Should be an array for additive text?
    public string[] next;

    Cutscene test;

    void Start()
    {
        test = new Cutscene();

        test.name = name;
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
