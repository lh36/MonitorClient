using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GetInstanceApi : MonoBehaviour{

    private string url = "get_instance";

    private UnityWebRequest www = null;
    private SignalCallback _callback;

    public bool isDone = false;


    public void AddCallback(SignalCallback _callback)
    {
        this._callback = _callback;
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
            Debug.Log (www.downloadHandler.text);
            InstanceJson instanceJson = JsonTool.JsonToClass<InstanceJson> (www.downloadHandler.text);

            if(instanceJson.status == true)
            {
                Dictionary<string, InstanceResp> resp = instanceJson.resp;
                //回调
                this._callback(null, resp);
            }    

            isDone = true;
        }

    }


}

//JSON解析类
public class InstanceJson
{
    public bool status = false;
    public Dictionary<string, InstanceResp> resp = null;

    public InstanceJson(){}
}

public class InstanceResp
{
    public long time;//时间戳
    public string name;//名称
    public string desp;//描述
    public int amount;//数量
    public Dictionary<string, int> shape;//船型

    public InstanceResp(){}
}