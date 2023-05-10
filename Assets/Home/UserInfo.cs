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
public class GetWordResponse
{
    public List<Item> words;
}

public static class UserInfo
{
    private static int userIdx;
    private static string parentPassword;
   
    public static void SetUserIdx(int idx)
    {
        userIdx = idx;
    }
    public static int GetUserIdx()
    {
        return userIdx;
    }
    public static void SetParentPassword(string p_password)
    {
        parentPassword = p_password;
    }
    public static string GetParentPassword()
    {
        return parentPassword;
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

            GetWordResponse data = JsonUtility.FromJson<GetWordResponse>(json);

            return data.words;
        }
        catch (WebException e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
