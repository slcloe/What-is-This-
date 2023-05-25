using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;


[Serializable]
public class ImageResult
{
    public ImageItem[] items;
}
[Serializable]
public class ImageItem
{
    public string link;
}

public class AmendsDirector : MonoBehaviour
{
    void Start()
    {
        GetImage();
        GameObject.Find("TextAmends").GetComponent<Text>().text = UserInfo.GetAmends().amends;
        GameObject.Find("ButtonBack").GetComponent<Button>().onClick.AddListener(GoBackToHome);
		if (LoginDirector.language == 0) SetKorText();
		else SetEngText();

	}
	void SetEngText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "We get present!");
	}
	void SetKorText()
	{
		SetLanguage.SetTextContent(SetText.texts[0], "선물을 받았어요!");
	}

	void GoBackToHome()
    {
        SceneManager.LoadScene("HomeScene");
        UserInfo.ResetAmendsRequest();
    }

    void GetImage()
    {
        Amends amends = UserInfo.GetAmends();
        if (!UserInfo.IsAmendsTime()) return;
        string text = amends.amends;
        
        string apiUrl = "https://openapi.naver.com/v1/search/image?query=" + text + "&display=1";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "GET";

        request.Headers["X-Naver-Client-Id"] = ApiKeys.Naver_Client_ID;
        request.Headers["X-Naver-Client-Secret"] = ApiKeys.Naver_Client_Secret;

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            if (json != null)
            {
                ImageResult data = JsonUtility.FromJson<ImageResult>(json);
                StartCoroutine(LoadImage(data.items[0].link));
            }
        }
        catch (WebException e)
        {
            Debug.Log(e);
        }
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            GameObject.Find("RawImage").GetComponent<RawImage>().texture 
                = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
