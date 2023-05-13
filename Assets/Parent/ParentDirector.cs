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
    
	//static private string apiUrl = "http://121.160.119.135:8081/getAnalysis/date/between?member_idx=1&older_date=2023-05-02T00:00:00&newer_date=2023-05-03T00:00:00";

	// Start is called before the first frame update
	void Start()
    {
		//Screen.orientation = ScreenOrientation.Portrait;   
		RequestAnalysisInfo();
	}

	void RequestAnalysisInfo()
	{
		analysis_info info = new analysis_info();
		//info.member_idx = UserInfo.GetUserIdx();
		info.member_idx = 1;
		info.older_date = "2023-05-02T00:00:00";
		info.newer_date = "2023-05-03T00:00:00";
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
