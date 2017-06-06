using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ConnectInstanceApi : MonoBehaviour
{

	private string url = "connect_instance/";

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
			HeartJson json = JsonTool.JsonToClass<HeartJson> (www.downloadHandler.text);

			if(json.status == true)
			{
				HeartResp resp = json.resp;
				//回调
				this._callback(null, resp.running);
			}    
		}

		isDone = true;
	}


}

//JSON解析类
public class HeartJson
{
	public bool status = false;
	public HeartResp resp = null;

	public HeartJson(){}
}

public class HeartResp
{
	public bool running = false;
}
