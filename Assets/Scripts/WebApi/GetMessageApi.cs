using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GetMessageApi
	{
		private string url = "get_fbmessage";
		private string m_sMessage = "";
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

        public bool IsRmtAllowed()
        {
            return (m_sMessage == "remote control on") ? true : false;
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
				m_sMessage = www.downloadHandler.text;
			}

			isDone = true;
		}

	}
}

