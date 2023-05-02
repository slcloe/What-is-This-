using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;


public class Level1Director : MonoBehaviour
{
    Text learning_word;
    Image learning_img;


    void Start()
    {
        learning_word = GameObject.Find("learning_word").GetComponent<Text>();
        learning_img = GameObject.Find("learning_img").GetComponent<Image>();

        string word = TensorFlowLite.SsdSample.detection_text;
        if (word == null ) { word = string.Empty; }
        learning_word.text = word;

        learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;
    }

    void Update()
    {
        
    }
}
