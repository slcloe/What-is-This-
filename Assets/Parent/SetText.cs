using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
	public static Text[] text;
	// Start is called before the first frame update
	void Start()
    {
		text = GetComponentsInChildren<Text>();
		Debug.Log(text.Length);
		for (int i = 0; i < text.Length; i++)
		{
			Debug.Log(text[i].text);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
