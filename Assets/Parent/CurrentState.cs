using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentState : MonoBehaviour
{
    Text curLevel;
    Text curWordCnt;
    // Start is called before the first frame update
    void Start()
    {
        curLevel = GameObject.Find("curLevel").GetComponent<Text>();
        curWordCnt = GameObject.Find("curWordCnt").GetComponent<Text>();

        curLevel.text = UserInfo.GetLevelAvg().ToString() + " Level";
        curWordCnt.text = UserInfo.GetcntWord().ToString() + " °³";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
