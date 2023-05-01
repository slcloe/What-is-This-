using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Net;


[Serializable]
public class SignUpInfo
{
    public string userId;
    public string password;
    public string name;
    public string birth;
    public string parentPassword;
}


public class SignUpDirector : MonoBehaviour
{
    InputField[] input = new InputField[4];
    Dropdown[] dropdown = new Dropdown[3];
    Button btSignUp;

    Text textDate;

    List<string> yearList = new List<string>();
    List<string> monthList = new List<string>();
    List<string> dayList = new List<string>();

    static private string apiUrl = "http://121.160.119.135:8081/signup";

    void Start()
    {
        input[0] = GameObject.Find("IDInputField").GetComponent<InputField>();
        input[1] = GameObject.Find("PWInputField").GetComponent<InputField>();
        input[2] = GameObject.Find("NameInputField").GetComponent<InputField>();
        input[3] = GameObject.Find("ParentPWInputField").GetComponent<InputField>();

        dropdown[0] = GameObject.Find("YearDropdown").GetComponent<Dropdown>();
        dropdown[1] = GameObject.Find("MonthDropdown").GetComponent<Dropdown>();
        dropdown[2] = GameObject.Find("DayDropdown").GetComponent<Dropdown>();
        InitDropdown();

        textDate = GameObject.Find("TextDate").GetComponent<Text>();

        btSignUp = GameObject.Find("ButtonSignUp").GetComponent<Button>();

        btSignUp.onClick.AddListener(CheckValidation);
    }

    void CheckValidation()
    {
/*        if (input[0].text == "") Toast.MakeToast("아이디를 입력해주세요.");
        else if (input[1].text == "") Toast.MakeToast("비밀번호를 입력해주세요.");
        else if (input[2].text == "") Toast.MakeToast("아이 이름을 입력해주세요.");
        else if (input[3].text == "") Toast.MakeToast("부모 비밀번호를 입력해주세요.");
        else */RequestSignUp();
    }

    void RequestSignUp()
    {
        SignUpInfo info = new SignUpInfo();
        info.userId = input[0].text;
        info.password = input[1].text;
        info.name = input[2].text;
        info.birth = yearList[dropdown[0].value] + "-" + monthList[dropdown[1].value].PadLeft(2, '0') + "-" + dayList[dropdown[2].value].PadLeft(2, '0');
        info.parentPassword = input[3].text;

        string str = JsonUtility.ToJson(info);

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;


        try
        {
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string text = reader.ReadToEnd();

            //Toast.MakeToast(text);
            if (response.StatusCode == HttpStatusCode.OK)
            {    
                SceneManager.LoadScene("LoginScene");
            } 
        }
        catch (WebException e)
        {
            //Toast.MakeToast("에러 발생");
        }
    }

    void InitDropdown()
    {
        string yearStr = DateTime.Now.ToString("yyyy");
        int yearInt = Convert.ToInt32(yearStr);

        for (int i = 0; i < 8; i++)
        {
            yearList.Add(yearInt.ToString());
            dropdown[0].options.Add(new Dropdown.OptionData(yearInt.ToString()));
            yearInt--;
        }

        for (int i = 1; i <= 12; i++)
        {
            monthList.Add(i.ToString());
            dropdown[1].options.Add(new Dropdown.OptionData(i.ToString()));
        }

        for (int i = 1; i <= 31; i++)
        {
            dayList.Add(i.ToString());
            dropdown[2].options.Add(new Dropdown.OptionData(i.ToString()));
        }

        dropdown[0].value = 0;
        dropdown[0].Select();
        dropdown[0].RefreshShownValue();
        dropdown[1].value = 0;
        dropdown[1].Select();
        dropdown[1].RefreshShownValue();
        dropdown[2].value = 0;
        dropdown[2].Select();
        dropdown[2].RefreshShownValue();
    }
}
