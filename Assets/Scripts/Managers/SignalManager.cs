using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalManager : SingletonUnity<SignalManager> {

	private Dictionary<MessageID, _MessageHandlerCollection> m_HandlerMap;
	private Queue<_Message> m_MessageQueue;


	/// <summary>
	/// 初始化消息管理器
	/// </summary>
	void Start()
	{
		this.m_HandlerMap = new Dictionary<MessageID, _MessageHandlerCollection>();
		this.m_MessageQueue = new Queue<_Message>();
	}

	/// <summary>
	/// 推动消息队列
	/// </summary>
	void Update()
	{
		if (this.m_MessageQueue == null || this.m_MessageQueue.Count == 0)
		{
			return;
		}

		var msg = this.m_MessageQueue.Dequeue();
		this.m_HandlerMap[msg.ID].DispatchMessage(msg.Sender, msg.Param);
	}

	/// <summary>
	/// 释放消息管理器的资源
	/// </summary>
	public override void Dispose()
	{
		this.m_MessageQueue.Clear();
		this.m_MessageQueue = null;

		foreach (var pair in this.m_HandlerMap)
		{
			pair.Value.Dispose();
		}

		this.m_HandlerMap.Clear();
		this.m_HandlerMap = null;
	}

	/// <summary>
	/// 添加一个消息接收器
	/// </summary>
	/// <param name="pMsgID">消息ID</param>
	/// <param name="pCallback">接受到消息的回调</param>
	public void AddHandler(MessageID pMsgID, MessageCallback pCallback)
	{
		if (pCallback == null)
		{
			return;
		}

		if (this.m_HandlerMap.ContainsKey(pMsgID))
		{
			this.m_HandlerMap[pMsgID].AddHandler(pCallback);
		}
		else
		{
			var mhc = new _MessageHandlerCollection();
			mhc.AddHandler(pCallback);
			this.m_HandlerMap.Add(pMsgID, mhc);
		}
	}

	/// <summary>
	/// 移除指定消息接收器
	/// </summary>
	/// <param name="pMsgID">消息ID</param>
	/// <param name="pCallback">添加时的回调</param>
	public void RemoveHandler(MessageID pMsgID, MessageCallback pCallback)
	{
		if (pCallback == null)
		{
			return;
		}

		if (this.m_HandlerMap.ContainsKey(pMsgID))
		{
			var mhc = this.m_HandlerMap[pMsgID];
			mhc.RemoveHandler(pCallback);

			if (mhc.Count == 0)
			{
				this.m_HandlerMap.Remove(pMsgID);
			}
		}
	}

	/// <summary>
	/// 向所有注册的接收器分发指定消息
	/// </summary>
	/// <param name="pMsgID">消息ID</param>
	/// <param name="pSender">消息发送者</param>
	/// <param name="pParam">消息参数</param>
	public void DispatchMessage(MessageID pMsgID, object pSender, object pParam = null)
	{
		if (!this.m_HandlerMap.ContainsKey(pMsgID))
		{ 
			return;
		}

		var m = new _Message()
		{
			ID = pMsgID,
			Sender = pSender,
			Param = pParam
		};

		this.m_MessageQueue.Enqueue(m);
	}


	private class _Message
	{
		public MessageID ID;
		public object Sender;
		public object Param;
	}


	private class _MessageHandlerCollection
	{
		public int Count { get { return this.m_HandlerList.Count; } }

		private List<MessageCallback> m_HandlerList;


		public _MessageHandlerCollection()
		{
			this.m_HandlerList = new List<MessageCallback>();
		}


		public void AddHandler(MessageCallback pCallback)
		{
			if (!this.m_HandlerList.Contains(pCallback))
			{
				this.m_HandlerList.Add(pCallback);
			}
		}


		public void RemoveHandler(MessageCallback pCallback)
		{
			this.m_HandlerList.Remove(pCallback);
		}


		public void DispatchMessage(object pSender, object pParam)
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

public delegate void MessageCallback(object pSender, object pParam);


//信号标记
public enum MessageID
{
	WindowA_ButtonClicked   
}