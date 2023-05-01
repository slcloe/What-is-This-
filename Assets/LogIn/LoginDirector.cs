using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Net;


[Serializable]
public class LoginInfo
{
    public string userId;
    public string password;
    public string parentPassword;
}


public class LoginDirector : MonoBehaviour
{
    InputField idInput;
    InputField pwInput;

    Button btLogin;
    Button btSignUp;

    static private string apiUrl = "http://121.160.119.135:8081/login";

    void Start()
    {
        idInput = GameObject.Find("IDInputField").GetComponent<InputField>();
        pwInput = GameObject.Find("PWInputField").GetComponent<InputField>();

        btLogin = GameObject.Find("ButtonLogin").GetComponent<Button>();
        btSignUp = GameObject.Find("ButtonSignUp").GetComponent<Button>();

        btLogin.onClick.AddListener(CheckValidation);
        btSignUp.onClick.AddListener(GotoSignUp);
    }

    void GotoSignUp()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    void CheckValidation()
    {
        /*        if (idInput.text == "") Toast.MakeToast("아이디를 입력해주세요.");
                else if (pwInput.text == "") Toast.MakeToast("비밀번호를 입력해주세요.");
                else */
        RequestLogin();
    }
    void RequestLogin()
    {
        LoginInfo info = new LoginInfo();
        info.userId = idInput.text;
        info.password = pwInput.text;

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

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Toast.MakeToast("로그인되었습니다.");
                SceneManager.LoadScene("HomeScene");
            }
            else
            {
                //Toast.MakeToast(text);
            }
        }
        catch (WebException e)
        {
            //Toast.MakeToast("에러 발생");
        }
    }
}
