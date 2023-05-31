using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;

public class LoadImage : MonoBehaviour
{
    Sprite captured;
    Image image;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = TensorFlowLite.ScreenCapture.detection_image;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
