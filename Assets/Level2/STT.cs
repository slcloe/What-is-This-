using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;
using System.Net;



[Serializable]
public class SetSTT
{
    public SetConfig config;
    public SetAudio audio;
}

[Serializable]
public class SetConfig
{
    public int sampleRateHertz;
    public string languageCode;
    public bool profanityFilter;
}
[Serializable]
public class SetAudio
{
    public string content;
}

[Serializable]
public class GetSTTContent
{
    public STTResult[] results;
}

[Serializable]
public class STTResult
{
    public STTAlternative[] alternatives;
}
[Serializable]
public class STTAlternative
{
    public string transcript;
    public float confidence;
}
public static class STT
{
    private static string apiUrl = "https://speech.googleapis.com/v1/speech:recognize?key="+ApiKeys.Google_API_key;
    static SetSTT stt;

    public static string SendAudio(string filePath)
    {
        InitData(filePath);
        string response = RequestSTT(stt);
        if (response != null)
        {
            GetSTTContent data = JsonUtility.FromJson<GetSTTContent>(response);
            if (data == null) return null;
            if (data.results == null) return null;
            STTAlternative alt = data.results[0].alternatives[0];
            if (alt == null) return null;
            Debug.Log("word: " + alt.transcript + " / confidence:" + alt.confidence);
            return alt.transcript;
        }
        else
        {
            return null;
        }
    }

    static void InitData(string filePath)
    {
        stt = new SetSTT();

        SetConfig config = new SetConfig();
        config.sampleRateHertz = 16000;
        config.languageCode = "ko-KR";
        config.profanityFilter = true;

        SetAudio audio = new SetAudio();
        audio.content = GetByte64StringFromFilePath(filePath);

        stt.config = config;
        stt.audio = audio;
    }
    static string GetByte64StringFromFilePath(string filePath)
    {
        FileStream fs = new FileStream(filePath + ".wav", FileMode.Open, FileAccess.Read);
       
        byte[] fileData = new byte[fs.Length];
        fs.Read(fileData, 0, fileData.Length);
        fs.Close();
        return Convert.ToBase64String(fileData);
    }

    static string RequestSTT(SetSTT obj)
    {
        string str = JsonUtility.ToJson(obj);

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

            return json;
        }
        catch (WebException e)
        {
            Debug.Log(e);
            return null;
        }
    }

}
