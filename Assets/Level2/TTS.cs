using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;



[Serializable]
public class SetTTS
{
    public SetInput input;
    public SetVoice voice;
    public SetAudioConfig audioConfig;
}

[Serializable]
public class SetInput
{
    public string text;
}
[Serializable]
public class SetVoice
{
    public string languageCode;
    public string name;
    public string ssmlGender;
}
[Serializable]
public class SetAudioConfig
{
    public string audioEncoding;
    public float speakingRate;
    public float pitch;
}

[Serializable]
class GetTTSContent
{
    public string audioContent;
}

public static class TTS
{

    static SetTTS tts;
    static private string apiUrl = "https://texttospeech.googleapis.com/v1/text:synthesize?key="+ApiKeys.Google_API_key;
    public static AudioClip GetAudio(string text)
    {
        InitData(text);
        return CreateAudio();
    }
    static void InitData(string text)
    {
        tts = new SetTTS();

        SetInput input = new SetInput();
        input.text = text;

        SetVoice voice = new SetVoice();
        voice.languageCode = "ko-KR";
        voice.name = "ko-KR-Neural2-A";
        voice.ssmlGender = "FEMALE";

        SetAudioConfig config = new SetAudioConfig();
        config.audioEncoding = "LINEAR16";
        config.speakingRate = 0.85f;
        config.pitch = -1.0f;

        tts.input = input;
        tts.voice = voice;
        tts.audioConfig = config;
    }

    static AudioClip CreateAudio()
    {
        string response = RequestTTS(tts);
        if (response != null)
        {
            GetTTSContent data = JsonUtility.FromJson<GetTTSContent>(response);

            byte[] bytes = Convert.FromBase64String(data.audioContent);
            float[] floats = ConvertByteToFloat(bytes);

            AudioClip audioClip = AudioClip.Create("audioContent", floats.Length, 1, 44100, false);
            audioClip.SetData(floats, 0);
            return audioClip;
        }
        else
        {
            return null;
        }
    }

    static float[] ConvertByteToFloat(byte[] array)
    {
        float[] floats = new float[array.Length / 2];
        for (int i = 0; i < floats.Length; i++)
        {
            floats[i] = BitConverter.ToInt16(array, i * 2) / 32768.0f;
        }
        return floats;
    }

    static string RequestTTS(SetTTS obj)
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
