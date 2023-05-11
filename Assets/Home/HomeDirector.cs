using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeDirector : MonoBehaviour
{

    ScrollRect scrollRect;
    Button btParent;

    HomePopUpDirector popupDirector;

    //0: vine, 1: stars, 2: ghosts, 3: clouds;
    GameObject[] objects = new GameObject[4];
    Vector2[] startPos = new Vector2[4];
    float[] curPos = new float[4];

    float velocity;
    float ratio;

    void Start()
    {
		scrollRect = GameObject.Find("ScrollArea").GetComponent<ScrollRect>();
        btParent = GameObject.Find("ButtonParent").GetComponent<Button>();
        popupDirector = GameObject.Find("PopUpDirector").GetComponent<HomePopUpDirector>();

        objects[0] = GameObject.Find("Vine1");
        objects[1] = GameObject.Find("Stars1");
        objects[2] = GameObject.Find("Ghosts1");
        objects[3] = GameObject.Find("Clouds1");

        startPos[0] = objects[0].transform.position;
        curPos[0] = 0;
        startPos[1] = objects[1].transform.position;
        curPos[1] = 0;
        startPos[2] = objects[2].transform.position;
        curPos[2] = 0;
        startPos[3] = objects[3].transform.position;
        curPos[3] = 0;

        ratio = startPos[0].x * (-1);

        btParent.onClick.AddListener(ShowPopUp);
    }
    void ShowPopUp()
    {
        popupDirector.ShowPopUp();
    }

    void Update()
    {
        velocity = scrollRect.velocity.x;

        RepeatBackground(0, 0.01f);
        RepeatBackground(1, 0.0003f);
        RepeatBackground(2, 0.001f);
        RepeatBackground(3, 0.0007f);
    }

    void RepeatBackground(int id, float percent)
    {
        curPos[id] = curPos[id] + velocity * percent * (ratio/2048);
        float newPos = curPos[id] % ratio;
        objects[id].transform.position = startPos[id] + Vector2.right * newPos;
    }

}
