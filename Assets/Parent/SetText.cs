using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
	public static Text[] parentText;
	// Start is called before the first frame update
	void Awake()
    {
		parentText = GetComponentsInChildren<Text>();
		Debug.Log(parentText.Length);
		for (int i = 0; i < parentText.Length; i++)
		{
			Debug.Log(i.ToString() + ": " + parentText[i].text);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
