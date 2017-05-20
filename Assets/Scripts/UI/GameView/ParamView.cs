using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ParamView : MonoBehaviour
{

    public Text T_Pos;
    public Text T_Rud;
    public Text T_Phi;
    public Text T_Speed;
    public Text T_LatLon;
    public Text T_Gear;
    public Text T_Time;

    private GameModel m_Model;
    private float m_fTime = 0;

    void Awake()
    {
        
    }

    void Start()
    {
		this.m_Model = this.gameObject.transform.parent.parent.gameObject.GetComponent<GameModel> ();
        SignalManager.Instance.AddHandler (SignalID.ShipParamChanged, this.ShowParam);

    }

    void FixedUpdate()
    {
        int s = (int)this.m_fTime % 60;
        int h = (int)this.m_fTime / 3600;
        int m = ((int)this.m_fTime % 3600) / 60;

        this.T_Time.text = h.ToString () + ":" + m.ToString () + ":" + s.ToString ();

        this.m_fTime += Time.fixedDeltaTime;
    }

    private void ShowParam(object oSender, object oParam)
    {
        int iShipID = (int)oSender;
        if (iShipID != this.m_Model.GetControlledShipID())
        {
            return;
        }

        SShipParam oShipParam = oParam as SShipParam;
        if(oShipParam == null)
        {
            return;
        }
		this.T_Pos.text = "(" + oShipParam.posX.ToString("0.000") + ", " + oShipParam.posY.ToString("0.000") + ")";
		this.T_Rud.text = oShipParam.rud.ToString ("0.000");
		this.T_Phi.text = oShipParam.phi.ToString ("0.000");
		this.T_Speed.text = oShipParam.speed.ToString ("0.000");
		this.T_LatLon.text = "(" + oShipParam.lat.ToString("0.000") + ", " + oShipParam.lon.ToString("0.000") + ")";
        this.T_Gear.text = oShipParam.gear.ToString ();

        long lInstanceTime = GlobalManager.Instance.GetInstanceData ().time;
        if(oShipParam.time > lInstanceTime)
        {
            this.m_fTime = oShipParam.time - GlobalManager.Instance.GetInstanceData ().time;
        }


    }

}

