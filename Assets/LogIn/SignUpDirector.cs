using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    Button btSignUp;

    static private string apiUrl = "http://121.160.119.135:8081/signup";

    void Start()
    {
        input[0] = GameObject.Find("IDInputField").GetComponent<InputField>();
        input[1] = GameObject.Find("PWInputField").GetComponent<InputField>();
        input[2] = GameObject.Find("NameInputField").GetComponent<InputField>();
        input[3] = GameObject.Find("ParentPWInputField").GetComponent<InputField>();
        btSignUp = GameObject.Find("ButtonSignUp").GetComponent<Button>();

        btSignUp.onClick.AddListener(requestSignUp);
    }

    void requestSignUp()
    {
        SignUpInfo info = new SignUpInfo();
        info.userId = input[0].text;
        info.password = input[1].text;
        info.name = input[2].text;
        info.birth = "2020-09-04";
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

            Debug.Log(text);
        }
        catch (WebException e)
        {
            Debug.Log(e);
        }
    }

}
