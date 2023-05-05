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
		string[] strs = new string[3];
		TensorFlowLite.Consonant cons = new TensorFlowLite.Consonant(word);
        //strs = cons.get_words();
        //option[0].text = strs[0];
        //option[1].text = strs[1];
        //option[2].text = strs[2];

        // Debug.Log("text1: " + strs[0]);
		//Debug.Log("text2: " + strs[1]);
		//Debug.Log("text3: " + strs[2]);
        
        option[0].text = cons.get_word(0);
		option[1].text = cons.get_word(1);
		option[2].text = cons.get_word(2);


		//option[0].text = word;
		//option[1].text = word;
		//option[2].text = word;
		learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;
	}

    void Update()
    {
        
    }
}
