using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;

[Serializable]
public class Item
{
    public int idx;
    public string word;
    public int level;
    public int successLevel;
    public string date;
    public Item(int idx, int successLevel)
    {
        this.idx = idx;
        this.successLevel = successLevel;
    }
}

[Serializable]
public class Word
{
    public int idx;
    public string word;
    public int level;
    public int successLevel;
    public string date;
}

[Serializable]
public class GetWordResponse
{
    public List<Item> wordList;
}

public static class UserInfo
{
    private static int userIdx;
   
    public static void SetUserIdx(int idx)
    {
        userIdx = idx;
    }
    public static int GetUserIdx()
    {
        return userIdx;
    }

    public static List<Item> GetWords()
    {
        string apiUrl = "http://121.160.119.135:8081/getWords?member_idx=" + userIdx;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "GET";

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            string json2 = "{\"wordList\":" + json + "}";

            GetWordResponse data = JsonUtility.FromJson<GetWordResponse>(json2);

            return data.wordList;
        }
        catch (WebException e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
