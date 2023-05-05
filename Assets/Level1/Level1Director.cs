using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;


public class Level1Director : MonoBehaviour
{
    Text learning_word;
    Image learning_img;
    Text[] option = new Text[3];
    string word = TensorFlowLite.SsdSample.detection_text;
    Consonant consonant;

    void Start()
    {
        learning_word = GameObject.Find("learning_word").GetComponent<Text>();
        learning_img = GameObject.Find("learning_img").GetComponent<Image>();

        option[0] = GameObject.Find("Button_learn1").GetComponentInChildren<Text>();
        option[1] = GameObject.Find("Button_learn2").GetComponentInChildren<Text>();
        option[2] = GameObject.Find("Button_learn3").GetComponentInChildren<Text>();

        //
        //if (word == null ) { word = string.Empty; }
        //learning_word.text = word;
        //consonant = new Consonant(word);
        //string[] strs = new string[3];
        //strs = consonant.get_words();
        //option[0].text = strs[0];
        //option[1].text = strs[1];
        //option[2].text = strs[2];

        option[0].text = word;
        option[1].text = word;
        option[2].text = word;

        learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;
    }

    void Update()
    {
        
    }
}
