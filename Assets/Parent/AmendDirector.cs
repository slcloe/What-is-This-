using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using UnityEditor;

[Serializable]
public class AmendUser
{
	public int member_idx;
}

[Serializable]
public class AmendInfo
{
	public long idx;
	public string amends;
	public int goal;
	public int remain;
}

public class AmendGetInfo
{
	public long idx;
	public string amends;
	public int goal;
	public int remain;
	public AmendGetInfo(long idx, string amends, int goal, int remain) 
	{
		this.idx = idx;
		this.amends = amends;
		this.goal = goal;
		this.remain = remain;
	}
	public AmendGetInfo(AmendInfo res)
	{
		res.idx = idx;
		res.amends = amends;
		res.goal = goal;
		res.remain = remain;
	}
}

public class AmendDirector : MonoBehaviour
{
	AmendGetInfo aminfo;
	InputField inputprize;
	Text placeholder;
	Text prize_name;
	List<string> cntList = new List<string>();
	// Start is called before the first frame update
	void Start()
    {
        inputprize = GameObject.Find("inputPrize").GetComponent<InputField>();

		aminfo = GetAmendInfo();
		if (aminfo.amends == null)
			GameObject.Find("inputPrize").GetComponent<InputField>().placeholder.GetComponent<Text>().text = "상품을 입력해주세요.";
		else
			GameObject.Find("inputPrize").GetComponent<InputField>().placeholder.GetComponent<Text>().text = aminfo.amends;

		if (aminfo.remain == 0)
			GameObject.Find("inputPeriod").GetComponent<InputField>().placeholder.GetComponent<Text>().text = "상품수여주기를 입력해주세요.";
		else
			GameObject.Find("inputPeriod").GetComponent<InputField>().placeholder.GetComponent<Text>().text = aminfo.remain.ToString();
	}

	AmendGetInfo GetAmendInfo()
	{
		AmendUser info = new AmendUser();
		info.member_idx = UserInfo.GetUserIdx();

		string apiUrl = "http://ec2-43-201-246-145.ap-northeast-2.compute.amazonaws.com:8081/getAmends?member_idx="+ info.member_idx;

		string str = JsonUtility.ToJson(info);
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
		request.Method = "GET";

		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			//Debug.Log("read");
			string json = reader.ReadToEnd();

			AmendInfo result = JsonUtility.FromJson<AmendInfo>(json);
			return new AmendGetInfo(result);
		}
		catch (WebException e)
		{
			return new AmendGetInfo(0, "0", 0, 0);
		}
	}

	void SendAmendInfo()
	{
		string apiUrl = "http://ec2-43-201-246-145.ap-northeast-2.compute.amazonaws.com:8081/amends/update";

		AmendUser info = new AmendUser();
		info.member_idx = UserInfo.GetUserIdx();


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

			string json = reader.ReadToEnd();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				LoginResponse data = JsonUtility.FromJson<LoginResponse>(json);
				UserInfo.SetUserIdx(data.idx);
				UserInfo.SetUserId(data.userId);
				UserInfo.GetLevel();
			}
			else
			{
				//Toast.MakeToast(text);
			}
		}
		catch (WebException e)
		{
			//Toast.MakeToast("에러 발생");
		}
	}
}
