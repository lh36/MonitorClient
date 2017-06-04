using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawManager : SingletonUnity<DrawManager>
{
	public float Height = 0.1f;
	public int CirclePointCount = 10;
	public int RequestFPS = 5;
	public LineRenderer m_RefLineRenderer;
	public LineRenderer m_MapLineRenderer;
	public LineRenderer m_ShipLineRenderer;
	public LineRenderer m_ControlLineRenderer;
	public LineRenderer m_ControlCircleRenderer;

	private int m_iUpdateTime = 0;
	private GetRefLineApi m_GetRefLineApi;
	private Dictionary<int, RefLineData> m_RefLineDict;
	private Dictionary<int, List<Vector3>> m_ShipTrackDict;


	void Awake()
	{
		this.m_GetRefLineApi = new GetRefLineApi ();
		this.m_GetRefLineApi.AddCallback (this.SetRefLineData);

	}

	// Use this for initialization
	void Start ()
	{
		this.m_RefLineDict = new Dictionary<int, RefLineData> ();
		this.m_ShipTrackDict = new Dictionary<int, List<Vector3>> ();
		this.m_RefLineRenderer.SetWidth (0.2f, 0.2f);

		SignalManager.Instance.AddHandler (SignalID.GameView_ControlChanged, this.DrawCallback);
		SignalManager.Instance.AddHandler (SignalID.ShipParamChanged, this.TrackDraw);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.m_iUpdateTime += 1;

		if(! GlobalManager.Instance.IsGameRunning)
		{
			return;
		}

		if (this.m_iUpdateTime >= this.RequestFPS)
		{
			if (this.m_GetRefLineApi.IsIdle())
			{
				StartCoroutine (this.m_GetRefLineApi.Request(GlobalManager.Instance.GetInstanceID()));
				this.m_iUpdateTime = 0;
			}
		}



	}

	private void DrawCallback(object oSender, object oParam)
	{
		Draw ();
	}

	private void Draw()
	{
		int iShapeID = ShipManager.Instance.ControlShipID;
		if(! this.m_RefLineDict.ContainsKey(iShapeID))
		{
			return;
		}

		var oRefLineData = this.m_RefLineDict [iShapeID];

		switch(oRefLineData.flag)
		{
		case 0:
			DrawNone ();
			break;
		case 1:
			DrawSpecialLine (oRefLineData);
			break;
		case 2:
			Vector3 v3CenterPoint = new Vector3 ((float)oRefLineData.posX, Height, (float)oRefLineData.posY);
			var pointsList = CalculatePoints (v3CenterPoint, (float)oRefLineData.radius);
			DrawCircle (this.m_RefLineRenderer, pointsList);
			break;
		case 3:
			DrawlLine (oRefLineData);
			break;
		case 4:
			DrawlLine (oRefLineData);
			break;
		case 5:
			DrawPoint (oRefLineData);
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// 不画东西
	/// </summary>
	private void DrawNone()
	{
		this.m_RefLineRenderer.SetVertexCount (0);
	}

	/// <summary>
	/// 画特殊线
	/// </summary>
	/// <param name="oRefLineData">O reference line data.</param>
	private void DrawSpecialLine(RefLineData oRefLineData)
	{
		this.m_RefLineRenderer.SetVertexCount (2);
		this.m_RefLineRenderer.SetPosition (0, new Vector3 (0, Height, (float)oRefLineData.posY));
		this.m_RefLineRenderer.SetPosition (1, new Vector3 (GlobalManager.Instance.MapSize.x, Height, (float)oRefLineData.posY));
	}

	/// <summary>
	/// 画线
	/// </summary>
	/// <param name="oRefLineData">O reference line data.</param>
	private void DrawlLine(RefLineData oRefLineData)
	{
		var pointList = oRefLineData.points;
		var iCount = pointList.Count;
		if(iCount < 2)
		{
			return;
		}
		this.m_RefLineRenderer.SetVertexCount (iCount / 2);
		for(int i=0; i<iCount/2; i++)
		{
			this.m_RefLineRenderer.SetPosition (i, new Vector3 (float.Parse(pointList[2*i]), Height, float.Parse(pointList[2*i + 1])));
		}
	}

	/// <summary>
	/// 画点
	/// </summary>
	/// <param name="oRefLineData">O reference line data.</param>
	private void DrawPoint(RefLineData oRefLineData)
	{
		Vector3 v3CenterPoint = new Vector3 ((float)oRefLineData.posX, Height, (float)oRefLineData.posY);
		var pointsList = CalculatePoints (v3CenterPoint, 0.3f);
		DrawCircle (this.m_RefLineRenderer, pointsList);
	}

	/// <summary>
	/// 画圆
	/// </summary>
	/// <param name="oLineRenderer">O line renderer.</param>
	/// <param name="pointsList">Points list.</param>
	private void DrawCircle(LineRenderer oLineRenderer, List<Vector3> pointsList)
	{
		oLineRenderer.SetVertexCount (CirclePointCount + 1);

		for(int i=0; i < CirclePointCount; i++)
		{
			oLineRenderer.SetPosition (i, pointsList [i]);
		}
		if (pointsList.Count > 0)
		{
			oLineRenderer.SetPosition (CirclePointCount, pointsList [0]);
		}
	}

	private List<Vector3> CalculatePoints(Vector3 v3CenterPoint, float fRadius)
	{
		float angle = 360f / CirclePointCount;
		var pointsList = new List<Vector3> ();
		transform.position = v3CenterPoint;
		Vector3 v = transform.position + transform.forward * fRadius;
		pointsList.Add (v);
		Quaternion r = transform.rotation;
		for(int i=1; i < CirclePointCount; i++)
		{
			Quaternion q = Quaternion.Euler (r.eulerAngles.x, r.eulerAngles.y - (angle * i), r.eulerAngles.z);
			v = transform.position + (q * Vector3.forward) * fRadius;
			pointsList.Add (v);
		}

		return pointsList;
	}

	/// <summary>
	/// 设置参考线
	/// </summary>
	/// <param name="oSender">sender.</param>
	/// <param name="oData">RefLine Data</param>
	public void SetRefLineData(object oSender, object oData)
	{
		if(!GlobalManager.Instance.IsGameRunning)
		{
			return;
		}
		this.m_RefLineDict = oData as Dictionary<int, RefLineData>;

		Draw ();
	}

	private void TrackDraw(object oSender, object oParam)
	{
		var iShipID = (int)oSender;
		if(ShipManager.Instance.ControlShipID != iShipID)
		{
			return;
		}
		if(oParam != null)
		{
			if(! this.m_ShipTrackDict.ContainsKey(iShipID))
			{
				this.m_ShipTrackDict.Add (iShipID, new List<Vector3> ());
			}

			var trackList = this.m_ShipTrackDict [iShipID];
			var oShipParam = oParam as SShipParam;
			if(trackList.Count == 0)
			{
				trackList.Add (new Vector3 ((float)oShipParam.posX, Height, (float)oShipParam.posY));
			}
			else
			{
				if( (Mathf.Abs((float)oShipParam.posX - trackList[trackList.Count - 1].x) > 0.05) ||
					(Mathf.Abs((float)oShipParam.posY - trackList[trackList.Count - 1].z) > 0.05) )
				{
					trackList.Add (new Vector3 ((float)oShipParam.posX, Height, (float)oShipParam.posY));
				}
			}
		}

		if(! this.m_ShipTrackDict.ContainsKey(iShipID))
		{
			return;
		}

		var iLength = this.m_ShipTrackDict[iShipID].Count;
		this.m_ShipLineRenderer.SetVertexCount (iLength);
		for(int i=0; i < iLength; i++)
		{
			this.m_ShipLineRenderer.SetPosition (i, this.m_ShipTrackDict[iShipID][i]);
		}
	}

	public void ClearTrack(int iShipID)
	{
		if(this.m_ShipTrackDict.ContainsKey(iShipID))
		{
			this.m_ShipTrackDict [iShipID].Clear ();
		}
		TrackDraw (iShipID, null);
	}

	public void DrawControlLine(ControlMode eMode)
	{
		switch(eMode)
		{
		case ControlMode.OpenControl:
			m_ControlLineRenderer.SetVertexCount (0);
			m_ControlCircleRenderer.SetVertexCount (0);
			break;
		case ControlMode.CircleControl:
			Vector3 v3CenterPoint = new Vector3 (24f, Height, 8f);
			var pointsList = CalculatePoints (v3CenterPoint, 4);
			DrawCircle (this.m_ControlLineRenderer, pointsList);
			pointsList = CalculatePoints (v3CenterPoint, 6);
			DrawCircle (this.m_ControlCircleRenderer, pointsList);
			break;
		default:
			m_ControlCircleRenderer.SetVertexCount (0);
			m_ControlLineRenderer.SetVertexCount (5);
			m_ControlLineRenderer.SetPosition (0, new Vector3 (2f, Height, 1f));
			m_ControlLineRenderer.SetPosition (1, new Vector3 (2f, Height, 14f));
			m_ControlLineRenderer.SetPosition (2, new Vector3 (46f, Height, 14f));
			m_ControlLineRenderer.SetPosition (3, new Vector3 (46f, Height, 1f));
			m_ControlLineRenderer.SetPosition (4, new Vector3 (2f, Height, 1f));
			break;
		}
	}
}

