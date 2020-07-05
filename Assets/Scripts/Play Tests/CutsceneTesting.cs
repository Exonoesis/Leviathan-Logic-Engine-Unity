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

    Cutscene test = new Cutscene();

    // Start is called before the first frame update
    void Start()
    {
        test.name = name;
        test.background = background;
        test.speaker = speaker;
        test.dialogue = dialogue;
        test.next = next;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            test.show();
        }
    }
}
