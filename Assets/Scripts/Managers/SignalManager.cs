using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalManager : SingletonUnity<SignalManager> {

    private Dictionary<SignalID, _SignalHandlerCollection> m_HandlerMap;
    private Queue<_Signal> m_SignalQueue;


	/// <summary>
	/// 初始化信号管理器
	/// </summary>
	void Awake()
	{
		this.m_HandlerMap = new Dictionary<SignalID, _SignalHandlerCollection>();
		this.m_SignalQueue = new Queue<_Signal>();
	}

	/// <summary>
	/// 推动信号队列
	/// </summary>
	void Update()
	{
		if (this.m_SignalQueue == null || this.m_SignalQueue.Count == 0)
		{
			return;
		}

        var signal = this.m_SignalQueue.Dequeue();
		this.m_HandlerMap[signal.ID].DispatchSignal(signal.Sender, signal.Param);
	}

	/// <summary>
	/// 释放信号管理器的资源
	/// </summary>
	public void Dispose()
	{
		this.m_SignalQueue.Clear();
		this.m_SignalQueue = null;

		foreach (var pair in this.m_HandlerMap)
		{
			pair.Value.Dispose();
		}

		this.m_HandlerMap.Clear();
		this.m_HandlerMap = null;
	}

	/// <summary>
	/// 添加一个信号接收器
	/// </summary>
	/// <param name="pMsgID">消息ID</param>
	/// <param name="pCallback">接受到消息的回调</param>
	public void AddHandler(SignalID nSignalID, SignalCallback pCallback)
	{
		if (pCallback == null)
		{
			return;
		}

		if (this.m_HandlerMap.ContainsKey(nSignalID))
		{
			this.m_HandlerMap[nSignalID].AddHandler(pCallback);
		}
		else
		{
			var mhc = new _SignalHandlerCollection();
			mhc.AddHandler(pCallback);
			this.m_HandlerMap.Add(nSignalID, mhc);
		}
	}

	/// <summary>
	/// 移除指定的信号接收器
	/// </summary>
	/// <param name="nSignalID">消息ID</param>
	/// <param name="pCallback">添加时的回调</param>
	public void RemoveHandler(SignalID nSignalID, SignalCallback pCallback)
	{
		if (pCallback == null)
		{
			return;
		}

		if (this.m_HandlerMap.ContainsKey(nSignalID))
		{
			var mhc = this.m_HandlerMap[nSignalID];
			mhc.RemoveHandler(pCallback);

			if (mhc.Count == 0)
			{
				this.m_HandlerMap.Remove(nSignalID);
			}
		}
	}

	/// <summary>
	/// 向所有注册的接收器分发指定信号
	/// </summary>
	/// <param name="nSignalID">消息ID</param>
	/// <param name="oSender">消息发送者</param>
	/// <param name="oParam">消息参数</param>
	public void DispatchSignal(SignalID nSignalID, object oSender, object oParam = null)
	{
		if (!this.m_HandlerMap.ContainsKey(nSignalID))
		{ 
			return;
		}

		var m = new _Signal()
		{
			ID = nSignalID,
			Sender = oSender,
			Param = oParam
		};

		this.m_SignalQueue.Enqueue(m);
	}


	private class _Signal
	{
		public SignalID ID;
		public object Sender;
		public object Param;
	}


	private class _SignalHandlerCollection
	{
		public int Count { get { return this.m_HandlerList.Count; } }

		private List<SignalCallback> m_HandlerList;


		public _SignalHandlerCollection()
		{
			this.m_HandlerList = new List<SignalCallback>();
		}


		public void AddHandler(SignalCallback pCallback)
		{
			if (!this.m_HandlerList.Contains(pCallback))
			{
				this.m_HandlerList.Add(pCallback);
			}
		}


		public void RemoveHandler(SignalCallback pCallback)
		{
			this.m_HandlerList.Remove(pCallback);
		}


		public void DispatchSignal(object pSender, object pParam)
		{
			for (int i = 0, count = this.m_HandlerList.Count; i < count; i++)
			{
				this.m_HandlerList[i].Invoke(pSender, pParam);
			}
		}


		public void Dispose()
		{
			this.m_HandlerList.Clear();
			this.m_HandlerList = null;
		}
	}

}

public delegate void SignalCallback(object pSender, object pParam);


//信号标记
public enum SignalID
{
	InitView_Start,   

    SelectView_SetView,
}