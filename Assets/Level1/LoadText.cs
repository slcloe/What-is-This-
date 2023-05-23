using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;

public class LoadText : MonoBehaviour
{
    Text object_name;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        object_name = GetComponent<Text>();
        text = TensorFlowLite.SsdSample.detection_text;
        Debug.Log(text);
        if (text == null) { text = string.Empty; }
        object_name.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
