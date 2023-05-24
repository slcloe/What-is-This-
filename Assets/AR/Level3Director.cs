using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Director : MonoBehaviour
{
    AudioSource audioSource;

    Level3PopUpDirector popupDirector;

    public GameObject[] hearts;
    public GameObject[] brokenHearts;

    int life = 3;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        popupDirector = GameObject.Find("PopUpDirector").GetComponent<Level3PopUpDirector>();

        for (int i = 0; i < 3; i++) brokenHearts[i].SetActive(false);
        SpeakCommand();
    }

    void SpeakCommand()
    {
        audioSource.PlayOneShot(TTS.GetAudio(0, "숨어 있는 글자를 찾아보세요."));
    }

    void ReduceLife()
    {
        life--;
        hearts[life].SetActive(false);
        brokenHearts[life].SetActive(true);
        if(life == 0)
        {
            popupDirector.ShowPopUp(false);
        }
    }

}
