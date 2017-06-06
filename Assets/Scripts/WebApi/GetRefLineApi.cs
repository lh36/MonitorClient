using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GetRefLineApi{

	private string url = "get_refline/";

	private UnityWebRequest www = null;
	private SignalCallback _callback;

	public bool isDone = true;

	public void AddCallback(SignalCallback _callback)
	{
		this._callback = _callback;
	}

	/// <summary>
	/// The api is idle?
	/// </summary>
	public bool IsIdle()
	{
		return (www == null || isDone) ? true : false;
	}

	/// <summary>
	/// Request ship params.
	/// </summary>
	public IEnumerator Request(int iInstanceID)
	{
		isDone = false;
		string uri = Constant.BaseUrl + url + iInstanceID.ToString();

		www = UnityWebRequest.Get (uri);
		yield return www.Send();

		if(www.isError)
		{
			Debug.Log (www.error);
			www.Dispose ();//清理数据
			www = null;//设为空
		}
		else
		{
			Debug.Log (www.downloadHandler.text);
			RefLineJson json;
			try
			{
				json = JsonTool.JsonToClass<RefLineJson> (www.downloadHandler.text);
				if(json.status == true)
				{
					var dRefLineData = new Dictionary<int, RefLineData> ();
					foreach (var item in json.resp) 
					{
						dRefLineData.Add (int.Parse (item.Key), item.Value);
					}
					this._callback (null, dRefLineData);
				} 
			}
			catch
			{
				Debug.Log ("error");
			}
		}

		isDone = true;
	}

}

//JSON解析类
public class RefLineJson
{
	public bool status = false;
	public Dictionary<string, RefLineData> resp = null;
}