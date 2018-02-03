using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VGameModel : MonoBehaviour
{
    private int m_ControlShipID = 1;

    // Use this for initialization
    void Awake ()
    {
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

		// 主摄像机视角绑定
        CameraController.Instance.LookAtObject = ShipManager.Instance.GetShipObjectByID (iShipID);
        // 控制视图改变
        SignalManager.Instance.DispatchSignal (SignalID.GameView_ControlChanged, null, iShipID);
        // 参数改变
        SignalManager.Instance.DispatchSignal (SignalID.ShipParamChanged, this.m_ControlShipID, 
            ShipManager.Instance.GetShipObjectByID (this.m_ControlShipID).GetComponent<ShipModel> ().Param);

		ShipManager.Instance.ControlShipID = iShipID;
    }

    public int GetControlledShipID()
    {
        return this.m_ControlShipID;
    }

}

