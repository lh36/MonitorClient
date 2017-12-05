using UnityEngine;
using System.Collections;

public class ShipModel : MonoBehaviour
{
	private SShipParam m_Param;

    private SignalCallback _cbSetStatus;//实验数据改变的回调函数



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    //-----------------对外接口-----------------

    //参数索引器
    public SShipParam Param
    {
        set 
        {
            this.m_Param = value;
            this._cbSetStatus (null, value);
        }

        get {return this.m_Param;}
    }

    /// <summary>
    /// 设置状态设置监听
    /// </summary>
    /// <param name="_callback">Callback.</param>
    public void SetStatusListener(SignalCallback _callback)
    {
        this._cbSetStatus = _callback;
    }
}

