using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2PopUpDirector : MonoBehaviour
{
    GameObject popup;
    Text successFailure;
    GameObject textDirectionObject;
    GameObject btNextObject;

    AudioSource audioSource;
    public AudioClip success_audio;
    public AudioClip failure_audio;

    void Start()
    {
        successFailure = GameObject.Find("SuccessFailure").GetComponent<Text>();
        textDirectionObject = GameObject.Find("TextDirection");
        btNextObject = GameObject.Find("ButtonNext");
        popup = GameObject.Find("PopUpWindow");


        audioSource = GameObject.Find("Level2Audio").GetComponent<AudioSource>();


        popup.SetActive(false);
    }

    public void ShowPopUp(bool success)
    {
        Color color;
        popup.SetActive(true);
        if (success)
        {
            audioSource.PlayOneShot(success_audio);
			if (LoginDirector.language == 0)
				successFailure.text = "성공!";
			else
				successFailure.text = "success!";
			ColorUtility.TryParseHtmlString("#039508", out color);
            successFailure.color = color;
            if (UserInfo.GetUserLevel() == 2)
            {
                UserInfo.StudyWord(TensorFlowLite.SsdSample.detection_text.Replace("\r", ""), 2);
                btNextObject.GetComponent<Button>().onClick.AddListener(GotoObjectDetection);
				if (LoginDirector.language == 0)
					textDirectionObject.GetComponent<Text>().text = "처음으로";
				else
					textDirectionObject.GetComponent<Text>().text = "Go back";
			}
            else
            {
                btNextObject.GetComponent<Button>().onClick.AddListener(GotoLevel3);
            }
        }
        else
        {
            audioSource.PlayOneShot(failure_audio);
            UserInfo.StudyWord(TensorFlowLite.SsdSample.detection_text.Replace("\r", ""), 1);
			if (LoginDirector.language == 0)
				successFailure.text = "실패!";
			else
				successFailure.text = "fail!";
			ColorUtility.TryParseHtmlString("#E00010", out color);
            successFailure.color = color;
            textDirectionObject.SetActive(false);
            btNextObject.SetActive(false);
            Invoke("SwitchToFailure", 2);
        }
    }

    void SwitchToFailure()
    {
        textDirectionObject.SetActive(true);
		if (LoginDirector.language == 0)
			textDirectionObject.GetComponent<Text>().text = "처음으로";
		else
			textDirectionObject.GetComponent<Text>().text = "Go back";
		btNextObject.SetActive(true);
        btNextObject.GetComponent<Button>().onClick.AddListener(GotoObjectDetection);
    }

    void GotoLevel3()
    {
        //SceneManager.LoadScene("Level3Scene");
        Debug.Log("load level3 scene");
    }
    void GotoObjectDetection()
    { 
        SceneManager.LoadScene("SSD");
    }
}
