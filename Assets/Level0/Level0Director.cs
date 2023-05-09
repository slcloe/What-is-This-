using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level0Director : MonoBehaviour
{

    AudioSource audioSource;

    Button btSound;
    Text textWord;
    Image learning_img;

    string word;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        btSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        textWord = GameObject.Find("TextWord").GetComponent<Text>();
		learning_img = GameObject.Find("learning_img").GetComponent<Image>();

		learning_img.sprite = TensorFlowLite.ScreenCapture.detection_image;

		SetWord();
        SetOnClickListener();
        SpeakCommand();
        Invoke("SpeakSound", 2);
    }

    void SetWord()
    {
        string getword = TensorFlowLite.SsdSample.detection_text;
        word = getword;
        textWord.text = word;
    }

    void SetOnClickListener()
    {
        btSound.onClick.AddListener(SpeakSound);
    }

    void SpeakSound()
    {
        audioSource.PlayOneShot(TTS.GetAudio(word));
    }

    void SpeakCommand()
    {
        audioSource.PlayOneShot(TTS.GetAudio("ÀÌ°Ç ¹¹Áö?"));
    }
}
