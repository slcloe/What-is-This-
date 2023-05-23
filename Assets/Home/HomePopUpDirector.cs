using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Net;

[Serializable]
public class Logininfo_p
{
    public string userId;
    public string password;
    public string parentPassword;
}

[Serializable]
public class LoginResponse_p
{
    public List<data> analysis;
}

[Serializable]
public class data
{
    public double successRate3;
	public int level;
	public int count;
	public long idx;
    public double successRate1;
    public double successRate2;
}


public class HomePopUpDirector : MonoBehaviour
{
    GameObject popup;
    Button btExitPopUp;
    Button btNext;
    Text text;
    InputField passwordInput;

    static private string apiUrl = "http://ec2-43-201-246-145.ap-northeast-2.compute.amazonaws.com:8081/plogin";
    void Start()
    {
        btExitPopUp = GameObject.Find("ButtonExitPopUp").GetComponent<Button>();
        btNext = GameObject.Find("ButtonNext").GetComponent<Button>();
        popup = GameObject.Find("PopUpWindow");
        passwordInput = GameObject.Find("PasswordInputField").GetComponent<InputField>();
        text = GameObject.Find("PopupText").GetComponent<Text>();
        if (LoginDirector.language == 0)
            text.text = "부모 비밀번호를\n입력해주세요.";
        else
            text.text = "Please enter\nparent password";
		popup.SetActive(false);
    }

    public void ShowPopUp()
    {
        popup.SetActive(true);
        btExitPopUp.onClick.AddListener(HidePopUp);
        btNext.onClick.AddListener(CheckValidation);
    }
    public void HidePopUp()
    {
        popup.SetActive(false);
    }

    void CheckValidation()
    {
        //if (passwordInput.text == "") { Toast.MakeToast("부모 비밀번호를 입력해주세요"); }
        RequestLogin();
    }
	void SetInfo(LoginResponse_p data)
	{
		UserInfo.SetSuccessRate1(data.analysis[0].successRate1);
		UserInfo.SetSuccessRate2(data.analysis[0].successRate2);
		UserInfo.SetSuccessRate3(data.analysis[0].successRate3);
		UserInfo.SetLevelAvg(data.analysis[0].level);
        UserInfo.SetcntWord(data.analysis[0].count);
	}
	void RequestLogin()
    {
        Logininfo_p info = new Logininfo_p();
        info.userId = UserInfo.GetUserId();
        Debug.Log(info.userId);
        info.parentPassword = passwordInput.text;
		Debug.Log(info.parentPassword);
		string str = JsonUtility.ToJson(info);

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "POST";
        request.ContentType = "application/json";
		request.ContentLength = bytes.Length;

        try
        {
            using(var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());

			string json = reader.ReadToEnd();

			Debug.Log(json);

			if (response.StatusCode == HttpStatusCode.OK)
            {
                //Toast.MakeToast("로그인되었습니다.");
                Debug.Log("로그인성공");
                LoginResponse_p data = JsonUtility.FromJson<LoginResponse_p>(json);
                SetInfo(data);
				SceneManager.LoadScene("ParentScene");

			}
			else
            {
                Debug.Log("로그인실패");
                //Toast.MakeToast(text);
            }
        }
        catch(WebException e)
        {
            Debug.Log("에러발생");
			//Toast.MakeToast("에러발생");
		}
    }



    public void GoToParentScene()
    {
        UserInfo.SetParentPassword("1234");
        if (passwordInput.text.Equals(UserInfo.GetParentPassword()))
        {
            Debug.Log("parent scene으로 이동");
            //SceneManager.LoadScene("ParentScene");
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다");
            //Toast.MakeToast("비밀번호가 틀렸습니다.");
        }
    }

}
