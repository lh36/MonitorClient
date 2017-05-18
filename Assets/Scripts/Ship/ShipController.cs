using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public float RotateLerp = 3f;   //旋转差值速度
    public float MoveLerp = 1f;     //移动差值速度

    private ShipModel m_Model;      //船Model

    private float m_fCurrentPhiAng = 0;     //当前航向角
    private float m_fDirPhiAng = 0;         //目的航向角
    private Vector3 m_v3DirPos = new Vector3();         //目的位置

    void Awake()
    {
        this.m_Model = gameObject.GetComponent<ShipModel> ();
        this.m_Model.SetStatusListener (this.SetShipStatus);
    }

    void Start()
    {
        Init ();
    }

    void FixedUpdate()
    {
        SetShipRudAng ();
        SetShipPos ();
    }

    /// <summary>
    /// 控制器初始化.
    /// </summary>
    public void Init()
    {
        
    }


    public void SetShipStatus(object oSender, object oParam)
    {
        SShipParam oShipParam = oParam as SShipParam;
        //位置
        this.m_v3DirPos = new Vector3 ((float)oShipParam.posX, 0, (float)oShipParam.posY);
        //舵角
        this.m_fDirPhiAng = (float)oShipParam.phi;
    }

    /// <summary>
    /// Sets the ship rud ang.
    /// </summary>
    private void SetShipRudAng()
    {
		Quaternion currentRotation = this.gameObject.transform.localRotation;
		Quaternion dirRotation = Quaternion.Euler (-90, 0, -this.m_fDirPhiAng);
		this.gameObject.transform.localRotation = Quaternion.Lerp (currentRotation, dirRotation, RotateLerp * Time.deltaTime);
    }

    /// <summary>
    /// Sets the ship position.
    /// </summary>
    private void SetShipPos()
    {
        Vector3 v3Pos = Vector3.Lerp (this.gameObject.transform.localPosition, this.m_v3DirPos, MoveLerp * Time.deltaTime);
        this.gameObject.transform.localPosition = v3Pos;
    }

}

