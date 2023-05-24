using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;


public class Level1Director : MonoBehaviour
{
    AudioSource audioSource;
    Text learning_word;
    Image learning_img;
    Text[] option = new Text[3];
    Button[] btn_option = new Button[3];
    string word = TensorFlowLite.SsdSample.detection_text;
    bool success = false;

    Level1PopUpDirector popupDirector;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        learning_word = GameObject.Find("learning_word").GetComponent<Text>();
        learning_img = GameObject.Find("learning_img").GetComponent<Image>();

        option[0] = GameObject.Find("Button_learn1").GetComponentInChildren<Text>();
        option[1] = GameObject.Find("Button_learn2").GetComponentInChildren<Text>();
        option[2] = GameObject.Find("Button_learn3").GetComponentInChildren<Text>();
        btn_option[0] = GameObject.Find("Button_learn1").GetComponent<Button>();
        btn_option[1] = GameObject.Find("Button_learn2").GetComponent<Button>();
        btn_option[2] = GameObject.Find("Button_learn3").GetComponent<Button>();

        popupDirector = GameObject.Find("PopUpDirector").GetComponent<Level1PopUpDirector>();

        SpeakCommand();
        //
        //if (word == null ) { word = string.Empty; }
        //learning_word.text = word;
        string[] strs = new string[3];
		TensorFlowLite.Consonant cons = new TensorFlowLite.Consonant(word);
        
        option[0].text = cons.get_word(0);
		option[1].text = cons.get_word(1);
		option[2].text = cons.get_word(2);

		learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;
        SetOnClickListener();

		if (LoginDirector.language == 0)
			SetKorText();
		else
			SetEngText();


	}

    void SetKorText()
    {
        SetLanguage.SetTextContent(SetText.texts[0], "맞는 단어를 찾아보세요");
		SetLanguage.SetTextContent(SetText.texts[5], "다음 단계로");
		SetLanguage.SetTextContent(SetText.texts[6], "그만할래요");
	}
    void SetEngText()
    {
		SetLanguage.SetTextContent(SetText.texts[0], "Find the right word");
		SetLanguage.SetTextContent(SetText.texts[5], "Next Step");
		SetLanguage.SetTextContent(SetText.texts[6], "Go Back");
	}

    void SpeakCommand()
    {
		if (LoginDirector.language == 0)
			audioSource.PlayOneShot(TTS.GetAudio("맞는 단어를 찾아보세요."));
        else
			audioSource.PlayOneShot(TTS.GetAudio("Find the right word"));
	}

    void SetOnClickListener()
    {
        btn_option[0].onClick.AddListener(CheckIsCorrect_0);
        btn_option[1].onClick.AddListener(CheckIsCorrect_1);
        btn_option[2].onClick.AddListener(CheckIsCorrect_2);
    }

    void CheckIsCorrect_0()
    {
        if (option[0].text.Equals(word))
            success = true;
        else
            success = false;
		popupDirector.ShowPopUp(success);
	}
	void CheckIsCorrect_1()
	{
		if (option[1].text.Equals(word))
			success = true;
		else
			success = false;
		popupDirector.ShowPopUp(success);
	}
	void CheckIsCorrect_2()
	{
		if (option[2].text.Equals(word))
			success = true;
		else
			success = false;
		popupDirector.ShowPopUp(success);
	}
    //void CheckSuccess()
    //{
    //    if (success)
    //    {

    //    }
    //    else
    //    {

    //    }
    //}
}
