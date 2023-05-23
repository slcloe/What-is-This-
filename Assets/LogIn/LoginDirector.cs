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

[Serializable]
public class LoginResponse
{
    public int idx;
    public string userId;
    public string password;
    public string name;
    public string birth;
    public string parentPassword;

}

public class LoginDirector : MonoBehaviour
{
    InputField idInput;
    InputField pwInput;
	Button btLogin;

    static private string apiUrl = "http://ec2-43-201-246-145.ap-northeast-2.compute.amazonaws.com:8081/login";
	public static int language = 0;
	void Start()
    {
        idInput = GameObject.Find("IDInputField").GetComponent<InputField>();
        pwInput = GameObject.Find("PWInputField").GetComponent<InputField>();

        btLogin = GameObject.Find("ButtonLogin").GetComponent<Button>();

        btLogin.onClick.AddListener(CheckValidation);

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

            string json = reader.ReadToEnd();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Toast.MakeToast("로그인되었습니다.");
                SceneManager.LoadScene("HomeScene");

                LoginResponse data = JsonUtility.FromJson<LoginResponse>(json);
                UserInfo.SetUserIdx(data.idx);
                UserInfo.SetUserId(data.userId);
                UserInfo.GetLevel();
                UserInfo.GetAmendsRequest();
                UserInfo.SetName(data.name);
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
