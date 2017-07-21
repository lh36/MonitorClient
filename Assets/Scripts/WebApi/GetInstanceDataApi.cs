using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GetInstanceDataApi{

    private string url = "get_data/";

    private UnityWebRequest www = null;
    private SignalCallback _callback;

    public bool isDone = true;

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
    public IEnumerator Request(int iInstanceID)
    {
        isDone = false;
        string uri = Constant.BaseUrl + url + iInstanceID.ToString();

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
            CollectionData oCollectionData;
            try
            {
                oCollectionData = JsonTool.JsonToClass<CollectionData> (www.downloadHandler.text);
                this._callback (null, oCollectionData);
            }
            catch
            {
                Debug.Log ("error");
            }
        }

        isDone = true;

    }

    public float GetProgress()
    {
        if(this.www == null)
        {
            return 0;
        }

        return this.www.downloadProgress;
    }

}

//JSON解析类
public class CollectionData
{
    public List<SShipParam> d1;
    public List<SShipParam> d2;
    public List<SShipParam> d3;
    public List<RefLineData> d1_ref;
    public List<RefLineData> d2_ref;
    public List<RefLineData> d3_ref;

}
