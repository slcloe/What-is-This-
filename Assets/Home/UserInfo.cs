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

[Serializable]
public class StudyWord
{
    public string word;
    public int level;
    public int successLevel;
    public string date;
    public StudyWord(string word, int level, int successLevel, string date)
    {
        this.word = word;
        this.level = level;
        this.successLevel = successLevel;
        this.date = date;
    }
}


public static class UserInfo
{
    private static int userIdx;
    private static string parentPassword;
    private static int userLevel;
   
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
    public static void SetUserLevel(int level)
    {
        userLevel = level;
    }
    public static int GetUserLevel()
    {
        return userLevel;
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
    public static void GetLevel()
    {
        string apiUrl = "http://121.160.119.135:8081/getLevel?member_idx=" + userIdx;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "GET";

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            
            userLevel = Convert.ToInt32(reader.ReadToEnd());
        }
        catch (WebException e)
        {
            Debug.Log(e);
        }
    }
    public static void StudyWord(string word, int successLevel)
    {

        string apiUrl = "http://121.160.119.135:8081/studyWord?member_idx=" + userIdx;

        string date = "2023-05-03T17:49:12";
        StudyWord item = new StudyWord(word, userLevel, successLevel, date);
        string str = JsonUtility.ToJson(item);


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


        }
        catch (WebException e)
        {
            Debug.Log(e);
        }

        GetLevel(); 
    }
}
