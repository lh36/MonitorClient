using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GetParamApi{

    private string url = "/ship/getparam";

    private Ship ship = null;
    private UnityWebRequest www = null;

    public bool isDone = false;

    /// <summary>
    /// Initializes a new instance of the api.
    /// </summary>
    /// <param name="ship">需要绑定的船舶对象</param>
    public GetParamApi(Ship ship)
    {
        this.ship = ship;//绑定船舶对象
    }

    /// <summary>
    /// The api is idle?
    /// </summary>
    public bool IsIdle()
    {
        return (www == null || isDone) ? true : false;
    }

    /// <summary>
    /// Request ship params.
    /// </summary>
    public IEnumerator Request()
    {
        isDone = false;
        string uri = Constant.BaseUrl + url;

        www = UnityWebRequest.Get (uri);
        yield return www.Send();

        if(www.isError)
        {
            Debug.Log (www.error);
            www.Dispose ();//清理数据
            www = null;//设为空
        }
        else
        {
            ParamJson paramJson = JsonTool.JsonToClass<ParamJson> (www.downloadHandler.text);

            Debug.Log (www.downloadHandler.text);

            if(paramJson.status == true)
            {
                ParamResp resp = paramJson.resp;
                //构造船舶参数结构体
                SShipParam param = new SShipParam (resp.lat, resp.lon, resp.posX, resp.posY, resp.rudAng, resp.traAng, resp.speed, resp.gear, resp.time);
                //设定参数
                ship.Param = param;
            }    

            isDone = true;
        }

    }


}

//JSON解析类
public class ParamJson
{
    public bool status = false;
    public ParamResp resp = null;

    public ParamJson(){}
}
public class ParamResp
{
    public float lat;//经度
    public float lon;//纬度
    public float posX;//X坐标
    public float posY;//Y坐标
    public float rudAng;//舵角
    public float traAng;//航迹角
    public float speed;//船速
    public int gear;//船速等级
    public long time;//运行时间

    public ParamResp(){}
}
