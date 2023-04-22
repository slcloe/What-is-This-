using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Director : MonoBehaviour
{
    AudioSource audioSource;

    Button btSound;
    Button btSpeech;
    Image timer;

    private float time_start;
    private float time_current;
    private float time_max = 5f;
    private bool isRecording = false;

    AudioClip recordClip = null;

    string filePath;

    void Start()
    {
        audioSource = GameObject.Find("Director").GetComponent<AudioSource>();
        btSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        btSpeech = GameObject.Find("ButtonSpeech").GetComponent<Button>();
        timer = GameObject.Find("Timer").GetComponent<Image>();

        SetOnClickListener();
        SpeakCommand();
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
                STT.SendAudio(filePath);
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
        audioSource.PlayOneShot(TTS.GetAudio("따라 말해보세요."));
    }

    void SpeakSound()
    {
        audioSource.PlayOneShot(TTS.GetAudio("고양이"));
    }
    void StartSpeech()
    {
        //말하기 버튼 비활성화
        btSpeech.interactable = false;

        //녹음 시작
        recordClip = Microphone.Start(Microphone.devices[0], false, 5, 16000);

        //타이머 시작시간 설정
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
