using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Level2Director : MonoBehaviour
{
    AudioSource audioSource;
    Level2PopUpDirector popupDirector;

    Button btSound;
    Button btSpeech;
    Image timer;
    Image learning_img;
    Text learning_word;

    private float time_start;
    private float time_current;
    private float time_max = 5f;
    private bool isRecording = false;

    AudioClip recordClip = null;

    string filePath;
    string word = TensorFlowLite.SsdSample.detection_text;


    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        popupDirector = GameObject.Find("PopUpDirector").GetComponent<Level2PopUpDirector>();
        btSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        btSpeech = GameObject.Find("ButtonSpeech").GetComponent<Button>();
        timer = GameObject.Find("Timer").GetComponent<Image>();
        learning_word = GameObject.Find("TextWord").GetComponent<Text>();
        learning_img = GameObject.Find("learning_img").GetComponent<Image>();

        learning_word.text = word;

        int fontSize = 200;
        if (word.Length > 3) fontSize -= (word.Length - 3) * 40;
        learning_word.fontSize = fontSize;

        learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;
        SetOnClickListener();
        SpeakCommand();
		if (LoginDirector.language == 0) SetKorText();
		else SetEngText();
	}
	void SetEngText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "Listen and Repeat");
		SetLanguage.SetTextContent(SetText.texts[2], "Audio");
		SetLanguage.SetTextContent(SetText.texts[3], "Record");
		SetLanguage.SetTextContent(SetText.texts[4], "Success!");
		SetLanguage.SetTextContent(SetText.texts[5], "Next Step");
		SetLanguage.SetTextContent(SetText.texts[6], "Go Back");
	}
	void SetKorText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "따라 말해보세요");
		SetLanguage.SetTextContent(SetText.texts[2], "소리 듣기");
		SetLanguage.SetTextContent(SetText.texts[3], "말하기");
		SetLanguage.SetTextContent(SetText.texts[4], "성공!");
		SetLanguage.SetTextContent(SetText.texts[5], "다음 단계로");
		SetLanguage.SetTextContent(SetText.texts[6], "그만할래요");
	}
	void Update()
    {
        if (isRecording)
        {
            time_current = Time.time - time_start;
            if (time_current <= time_max)
            {
                timer.fillAmount = time_current / time_max;
            }
            else
            {
                isRecording = false;
                btSpeech.interactable = true;
                StopRecording();
                string result = STT.SendAudio(filePath);
                if(result == null)
                {
					if (LoginDirector.language == 0)
						audioSource.PlayOneShot(TTS.GetAudio(0, "다시 말해보세요"));
                    else
						audioSource.PlayOneShot(TTS.GetAudio(1, "Say it again"));
				}
                else
                {
                    popupDirector.ShowPopUp(Regex.Replace(result, @"\s", "").Equals(Regex.Replace(word, @"\s", "")));
                }
            }
        }

    }

    void SetOnClickListener()
    {
        btSound.onClick.AddListener(SpeakSound);
        btSpeech.onClick.AddListener(StartSpeech);
    }

    void SpeakCommand()
    {
		if (LoginDirector.language == 0)
			audioSource.PlayOneShot(TTS.GetAudio(0, "따라 말해보세요."));
        else
            audioSource.PlayOneShot(TTS.GetAudio(1, "Listen and Repeat"));
    }

    void SpeakSound()
    {
        audioSource.PlayOneShot(TTS.GetAudio(0, word));
    }
    void StartSpeech()
    {
        btSpeech.interactable = false;
        recordClip = Microphone.Start(Microphone.devices[0], false, 5, 16000);
        ResetTimer();
    }
    void ResetTimer()
    {
        time_start = Time.time;
        time_current = 0;
        isRecording = true;
    }
    void StopRecording()
    {
        if (Microphone.IsRecording(Microphone.devices[0]))
        {
            Microphone.End(Microphone.devices[0]);

            if (recordClip == null)
            {
                return;
            }

            filePath = Application.persistentDataPath + "\\wordPronunciation";

            SavWav.Save(filePath, recordClip);
        }
    }
}
