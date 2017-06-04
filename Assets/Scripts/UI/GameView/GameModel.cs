using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : MonoBehaviour
{
    private int m_ControlShipID = 1;
	private SubmitControlApi m_ControlApi;

    // Use this for initialization
    void Awake ()
    {
		this.gameObject.AddComponent<SubmitControlApi> ();
		this.m_ControlApi = this.gameObject.GetComponent<SubmitControlApi> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    //对外接口

    /// <summary>
    /// 选择监控的船只
    /// </summary>
    /// <param name="iShipID">I ship I.</param>
    public void ChooseShip(int iShipID)
    {
        this.m_ControlShipID = iShipID;
        CameraController.Instance.LookAtObject = ShipManager.Instance.GetShipObjectByID (iShipID);

        //控制视图改变
        SignalManager.Instance.DispatchSignal (SignalID.GameView_ControlChanged, null, iShipID);
        //参数改变
        SignalManager.Instance.DispatchSignal (SignalID.ShipParamChanged, this.m_ControlShipID, 
            ShipManager.Instance.GetShipObjectByID (this.m_ControlShipID).GetComponent<ShipModel> ().Param);

		ShipManager.Instance.ControlShipID = iShipID;
    }

    public int GetControlledShipID()
    {
        return this.m_ControlShipID;
    }

	public void SubmitControl(string sControlData)
	{
		if(m_ControlApi.IsIdle())
		{
			StartCoroutine (m_ControlApi.Request (GlobalManager.Instance.GetInstanceID (), sControlData));
		}
	}

	/// <summary>
	/// 开环控制
	/// </summary>
    public void StartOpenControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
		InputManager.Instance.SetControlMode (ControlMode.OpenControl);
    }

	/// <summary>
	/// 目标点跟踪
	/// </summary>
    public void StartPointControl()
    {
		SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
		InputManager.Instance.SetControlMode (ControlMode.PointControl);
    }

    private void PointControl(object oSender, object oParam)
    {
        var v2Point = (Vector2) oParam;
		SubmitControl ("c&" + this.m_ControlShipID.ToString () + "&p&1&" + v2Point.x.ToString() + "&" + v2Point.y.ToString());
		InputManager.Instance.SetControlMode (ControlMode.PointControl);
    }

	/// <summary>
	/// 特殊直线跟踪
	/// </summary>
    public void StartSpecialLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.SpecialLineControl);
		InputManager.Instance.SetControlMode (ControlMode.SpecialLineControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
		SubmitControl ("c&" + this.m_ControlShipID.ToString () + "&l&" + ((float)oParam).ToString());
		InputManager.Instance.SetControlMode (ControlMode.SpecialLineControl);
    }

	/// <summary>
	/// 一般直线跟踪
	/// </summary>
    public void StartGenLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
		SignalManager.Instance.AddHandler (SignalID.ControlClick, this.GenLineControl);
		InputManager.Instance.SetControlMode (ControlMode.GenLineControl);
    }

	private void GenLineControl(object oSender, object oParam)
    {
        var pointList = oParam as List<Vector2>;
		SubmitControl ("c&" + this.m_ControlShipID.ToString () + "&g&2-" + 
			pointList[0].x.ToString() + "&" + pointList[0].y.ToString() + 
			"&" + pointList[1].x.ToString() + "&" + pointList[1].y.ToString()
		);
		InputManager.Instance.SetControlMode (ControlMode.GenLineControl);
    }

	/// <summary>
	/// 多端直线跟踪
	/// </summary>
    public void StartMulLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
		SignalManager.Instance.AddHandler (SignalID.ControlClick, this.MulLineControl);
		InputManager.Instance.SetControlMode (ControlMode.MulLineControl);
    }

	private void MulLineControl(object oSender, object oParam)
    {
		var pointList = oParam as List<Vector2>;
		var n = pointList.Count;
		if(n < 2)
		{
			return;
		}

		string sCommand = "c&" + this.m_ControlShipID.ToString () + "&m&" + n.ToString();
		for(int i=0; i < n; i++)
		{
			sCommand += "&" + pointList [i].x.ToString () + "&" + pointList [i].y.ToString ();
		}

        SubmitControl (sCommand);
		InputManager.Instance.SetControlMode (ControlMode.MulLineControl);
    }

	/// <summary>
	/// 圆轨迹跟踪
	/// </summary>
    public void StartCircleControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
		SignalManager.Instance.AddHandler (SignalID.ControlClick, this.CircleControl);
		InputManager.Instance.SetControlMode (ControlMode.CircleControl);
    }

	private void CircleControl(object oSender, object oParam)
    {
		SubmitControl ("c&" + this.m_ControlShipID.ToString () + "&r&" + ((float)oParam).ToString());
		InputManager.Instance.SetControlMode (ControlMode.CircleControl);
    }

	/// <summary>
	/// 编队航行
	/// </summary>
    public void StartFormationControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
		SignalManager.Instance.AddHandler (SignalID.ControlClick, this.FormationControl);
		InputManager.Instance.SetControlMode (ControlMode.FormationControl);
    }

	private void FormationControl(object oSender, object oParam)
    {
		SubmitControl ("c&4&f");
		InputManager.Instance.SetControlMode (ControlMode.FormationControl);
    }
}

