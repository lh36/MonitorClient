using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    private string TAG = "ShipController : ";

    public float RotateLerp = 1f;//旋转差值速度

    private Ship ship;//船
    private GetParamApi getParamApi;//获取参数Api

    private float fCurrentRudAng = 0;//当前舵角
    private float fDirRudAng = 0;//目的舵角

    void Start()
    {
        Init ();
    }

    void Update()
    {
        //进行船舶参数获取请求
//        if (getParamApi.IsIdle ()) 
//        {
//            StartCoroutine (getParamApi.Request ());
//        }
    }

    void FixedUpdate()
    {
        SetShipRudAng (fCurrentRudAng = Mathf.Lerp(fCurrentRudAng, fDirRudAng, RotateLerp * Time.deltaTime));
    }

    /// <summary>
    /// 控制器初始化.
    /// </summary>
    public void Init()
    {
        ship = new Ship (this);
        getParamApi = new GetParamApi (ship);
    }

    //自Ship索引器调用
    public void SetShipStatus()
    {
        //位置
        this.gameObject.transform.localPosition = new Vector3 (ship.Param.posX, 0, ship.Param.posY);
        //舵角
        fDirRudAng = ship.Param.rudAng;
    }

    private void SetShipRudAng(float rudAng)
    {
        this.gameObject.transform.rotation = Quaternion.Euler (-90, rudAng, 0);
    }



}

