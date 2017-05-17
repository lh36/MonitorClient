using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour
{
    private int m_ControlShipID = 0;
	private SubmitControlApi m_ControlApi;

    // Use this for initialization
    void Start ()
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
}

