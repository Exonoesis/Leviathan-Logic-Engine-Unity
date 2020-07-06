using UnityEngine;

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

        test.setSceneName(sceneName);
        test.setBackground(background);
        test.setSpeaker(speaker);
        test.setDialogue(dialogue);
        test.setNext(next);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            test.show();
        }
    }
}
