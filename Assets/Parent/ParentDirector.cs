using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;
using System.Reflection;
using UnityEditor;

[Serializable]
public class analysis_info
{
	public int member_idx;
	public string older_date;
	public string newer_date;
}

[Serializable]
public class analysis_result
{
	public int idx;
	public int count;
	public int level;
	public float successRate1;
	public float successRate2;
	public float successRate3;
}

public class ParentDirector : MonoBehaviour
{
	string[] days = new string[9];
	public List<float> successRate1 = new List<float>();
	public List<float> successRate2 = new List<float>();
	public List<float> successRate3 = new List<float>();

	// Start is called before the first frame update
	void Start()
    {
		//Screen.orientation = ScreenOrientation.Portrait;
		SetDays();
		for(int i=0;i<8;i++)
		{
			RequestAnalysisInfo(days[i], days[i+1]);
		}

	}

	void SetDays()
	{
		string time = "T00:00:00";
		days[0] = DateTime.Now.ToString("yyyy/MM/dd") + time;
		for (int i=0;i<8;i++)
		{
			days[i + 1] = DateTime.Now.AddDays(-7 * (i+1)).ToString("yyyy/MM/dd") + time;
		}

		for (int i=0;i<9;i++)
		{
			Debug.Log(days[i]);
		}
	}

	void RequestAnalysisInfo(string older, string newer)
	{
		analysis_info info = new analysis_info();
		//info.member_idx = UserInfo.GetUserIdx();
		info.member_idx = 1;
		info.older_date = older;
		info.newer_date = newer;
		string apiUrl = "http://121.160.119.135:8081/getAnalysis/date/between?member_idx=" + info.member_idx.ToString() + "&older_date="+info.older_date+"&newer_date="+info.newer_date;


		string str = JsonUtility.ToJson(info);
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
		request.Method = "GET";
		Debug.Log("get start");
		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			Debug.Log("read");
			string json = reader.ReadToEnd();

			analysis_result result = JsonUtility.FromJson<analysis_result>(json);
			Debug.Log(result.idx);
		}
		catch (WebException e)
		{

		}

	}
}
