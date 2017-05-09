using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour
{
    private int m_ControlShipID = 0;

    // Use this for initialization
    void Start ()
    {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    //对外接口

    /// <summary>
    /// Chooses the ship to control.
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
}

