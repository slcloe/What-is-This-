using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    public static int language = 0;
    Text[] text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentsInChildren<Text>();
        for (int i=0;i<text.Length; i++)
        {
           // Debug.Log(text[i].text);
        }
    }
    public void SetKor(bool isOn)
    {
        if (isOn) language = 1;
        else language = 0;
        Debug.Log(language);
    }
    public void SetEng(bool isOn)
    {
        if(isOn) language = 0;
        else language = 1;
		Debug.Log(language);
	}
}
