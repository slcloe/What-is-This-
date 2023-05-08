using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level0Director : MonoBehaviour
{

    AudioSource audioSource;

    Button btSound;
    Text textWord;

    string word;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        btSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        textWord = GameObject.Find("TextWord").GetComponent<Text>();

        SetWord();
        SetOnClickListener();
        SpeakCommand();
        Invoke("SpeakSound", 2);
    }

    void SetWord()
    {
        string getword = "고양이";
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
        audioSource.PlayOneShot(TTS.GetAudio("이건 뭐지?"));
    }
}
