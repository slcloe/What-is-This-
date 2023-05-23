using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    public static int language = 0;
    // Start is called before the first frame update
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
