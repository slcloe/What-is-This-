using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Director : MonoBehaviour
{
    AudioSource audioSource;

    public  Level3PopUpDirector popupDirector;

    public  GameObject[] hearts;
    public  GameObject[] brokenHearts;
	public  static Text curState;

    public int life = 3;
    public static int calllife = 3;
    public static bool success = false;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        popupDirector = GameObject.Find("PopUpDirector").GetComponent<Level3PopUpDirector>();
		curState = GameObject.Find("TextBox").GetComponentInChildren<Text>();

        curState.text = "";
		life = 3;
        for (int i = 0; i < 3; i++) brokenHearts[i].SetActive(false);
        SpeakCommand();
		if (LoginDirector.language == 0) SetKorText();
		else SetEngText();
	}
	void SetEngText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "Find hidden letters");
		SetLanguage.SetTextContent(SetText.texts[1], "Go Back");
		SetLanguage.SetTextContent(SetText.texts[3], "Success!");
		SetLanguage.SetTextContent(SetText.texts[4], "Go Back");
	}
	void SetKorText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "숨어 있는 글자를 찾아보세요");
		SetLanguage.SetTextContent(SetText.texts[1], "그만할래요");
		SetLanguage.SetTextContent(SetText.texts[3], "성공!");
		SetLanguage.SetTextContent(SetText.texts[4], "처음으로");
	}

	void SpeakCommand()
    {
		if (LoginDirector.language == 0)
			audioSource.PlayOneShot(TTS.GetAudio(0, "숨어 있는 글자를 찾아보세요."));
        else
			audioSource.PlayOneShot(TTS.GetAudio(1, "Find hidden letters"));

	}

	void ReduceLife()
    {
        life--;
        Debug.Log("LIFE : " + life.ToString()); 
        hearts[life].SetActive(false);
        brokenHearts[life].SetActive(true);
        if(life <= 0)
        {
            popupDirector.ShowPopUp(false);
        }
    }


    void Update()
    {
        if (success)
        {
            popupDirector.ShowPopUp(true);
        }

        if (calllife < life)
        {
            ReduceLife();
        }

    }


   }
