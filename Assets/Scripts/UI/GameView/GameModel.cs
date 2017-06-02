using UnityEngine;
using System.Collections;

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

    public void StartOpenControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
    }

    public void StartPointControl()
    {
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void PointControl(object oSender, object oParam)
    {
        var v2Point = oParam as Vector2;
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-p-1-" + v2Point.x.ToString() + "-" + v2Point.y.ToString());
    }

    public void StartSpecialLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-l-" + (float)oParam.ToString());
    }

    public void StartGenLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
        var v2Point = oParam as Vector2;
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-p-1-" + v2Point.x.ToString() + "-" + v2Point.y.ToString());
    }

    public void StartMulLineControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
        var v2Point = oParam as Vector2;
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-p-1-" + v2Point.x.ToString() + "-" + v2Point.y.ToString());
    }

    public void StartCircleControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
        var v2Point = oParam as Vector2;
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-p-1-" + v2Point.x.ToString() + "-" + v2Point.y.ToString());
    }

    public void StartFormationControl()
    {
        SignalManager.Instance.RemoveAllHandler (SignalID.ControlClick);
        SignalManager.Instance.AddHandler (SignalID.ControlClick, this.PointControl);
    }

    private void SpecialLineControl(object oSender, object oParam)
    {
        var v2Point = oParam as Vector2;
        SubmitControl ("c-" + this.m_ControlShipID.ToString () + "-p-1-" + v2Point.x.ToString() + "-" + v2Point.y.ToString());
    }
}

