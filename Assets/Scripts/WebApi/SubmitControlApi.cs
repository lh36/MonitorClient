using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SubmitControlApi : MonoBehaviour
{

	private string url = "control";

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
	public IEnumerator Request(int iInstanceID, string sControlData)
	{
		Debug.Log (iInstanceID);
		Debug.Log (sControlData);
		isDone = false;
		string uri = Constant.BaseUrl + url;
		WWWForm form = new WWWForm ();
		form.AddField ("instanceid", iInstanceID.ToString ());
		form.AddField ("control", sControlData);

		www = UnityWebRequest.Post (uri, form);
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
		}

		isDone = true;
	}
}

//JSON解析类
public class ControlJson
{
	public bool status = false;
	public object resp = null;
}