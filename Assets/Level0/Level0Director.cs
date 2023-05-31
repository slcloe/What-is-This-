using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level0Director : MonoBehaviour
{

    AudioSource audioSource;

    Button btSound;
    Button btNext;
    Text textWord;
    Image learning_img;

    string word;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        btSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        btNext = GameObject.Find("ButtonNext").GetComponent<Button>();
        textWord = GameObject.Find("TextWord").GetComponent<Text>();
		learning_img = GameObject.Find("learning_img").GetComponent<Image>();

		learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;

		SetWord();
        SetOnClickListener();
        SpeakCommand();
        Invoke("SpeakSound", 2);
        if (LoginDirector.language == 0) SetKorText();
        else SetEngText();

	}
	void SetEngText()
	{
		SetLanguage.SetTextContent(SetText.texts[1], "Audio");
		SetLanguage.SetTextContent(SetText.texts[2], "What is this?");
		SetLanguage.SetTextContent(SetText.texts[3], "Next Step");
		SetLanguage.SetTextContent(SetText.texts[4], "Go back");
	}
	void SetKorText()
	{
		SetLanguage.SetTextContent(SetText.texts[1], "소리듣기");
		SetLanguage.SetTextContent(SetText.texts[2], "이건 뭐지?");
		SetLanguage.SetTextContent(SetText.texts[3], "학습하러 가기");
		SetLanguage.SetTextContent(SetText.texts[4], "그만할래요");
	}
	void SetWord()
    {
        string getword = TensorFlowLite.SsdSample.detection_text;
        word = getword;
        textWord.text = word;
        int fontSize = 300;
        if (word.Length > 3) fontSize -= (word.Length - 3) * 40;
        textWord.fontSize = fontSize;
    }

    void SetOnClickListener()
    {
        btSound.onClick.AddListener(SpeakSound);
        btNext.onClick.AddListener(GoToLevel1);
    }
    
    void GoToLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    void SpeakSound()
    {
        audioSource.PlayOneShot(TTS.GetAudio(0, word));
    }

    void SpeakCommand()
    {
		if (LoginDirector.language == 0)
			audioSource.PlayOneShot(TTS.GetAudio(0, "이건 뭐지?"));
        else
			audioSource.PlayOneShot(TTS.GetAudio(1, "what is this?"));
	}
}
