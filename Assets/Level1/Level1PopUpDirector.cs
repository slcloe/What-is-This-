using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1PopUpDirector : MonoBehaviour
{
    GameObject popup;
    Text successFailure;
    GameObject textDirectionObject;
    GameObject btNextObject;

    void Start()
    {
        successFailure = GameObject.Find("SuccessFailure").GetComponent<Text>();
        textDirectionObject = GameObject.Find("TextDirection");
        btNextObject = GameObject.Find("ButtonNext");
        popup = GameObject.Find("PopUpWindow");


        popup.SetActive(false);
    }

    public void ShowPopUp(bool success)
    {
        Color color;
        popup.SetActive(true);
        if (success)
        {
            successFailure.text = "성공!";
            ColorUtility.TryParseHtmlString("#039508", out color);
            successFailure.color = color;
            btNextObject.GetComponent<Button>().onClick.AddListener(GotoLevel2);
        }
        else
        {
            successFailure.text = "실패!";
            ColorUtility.TryParseHtmlString("#E00010", out color);
            successFailure.color = color;
            textDirectionObject.SetActive(false);
            btNextObject.SetActive(false);
            Invoke("SwitchToFailure", 1);
        }
    }

    void SwitchToFailure()
    {
        textDirectionObject.SetActive(true);
        textDirectionObject.GetComponent<Text>().text = "처음으로";
        btNextObject.SetActive(true);
        btNextObject.GetComponent<Button>().onClick.AddListener(GotoObjectDetection);
    }

    void GotoLevel2()
    {
        SceneManager.LoadScene("Level2Scene");
    }
    void GotoObjectDetection()
    {
        SceneManager.LoadScene("SSD");
    }
}
