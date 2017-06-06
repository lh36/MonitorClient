using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GetVideoDataApi{

	private string url = "get_video";

	private UnityWebRequest www = null;
	private SignalCallback _callback;
	private int m_iWidth;
	private int m_iHeight;

	public bool isDone = true;


	public void AddCallback(int width, int height, SignalCallback _callback)
	{
		this.m_iWidth = width;
		this.m_iHeight = height;
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
	public IEnumerator Request()
	{
		isDone = false;
		string uri = Constant.BaseUrl + url;

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
			byte[] btData = www.downloadHandler.data;

			Texture2D texture = new Texture2D (this.m_iWidth, this.m_iHeight);
			texture.LoadImage (btData);
			_callback (null, texture);

		}

		isDone = true;

	}


}
