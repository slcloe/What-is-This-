using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
	public static Text[] texts;
	// Start is called before the first frame update
	void Awake()
    {
		texts = GetComponentsInChildren<Text>();
		Debug.Log(texts.Length);
		for (int i = 0; i < texts.Length; i++)
		{
			Debug.Log(i.ToString() + ": " + texts[i].text);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
