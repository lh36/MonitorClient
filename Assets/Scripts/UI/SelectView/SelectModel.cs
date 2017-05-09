using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModel : MonoBehaviour
{
    private GetInstanceApi m_GetInstanceNet;

    private Dictionary<int, InstanceResp> m_InstanceDict = new Dictionary<int, InstanceResp>();

    void Awake()
    {
        Init ();
    }

    void OnEnable()
    {
        RequestWebInstanceData ();
    }

    public void Init()
    {
        this.m_GetInstanceNet = new GetInstanceApi ();
        this.m_GetInstanceNet.AddCallback (this.SetInstanceData);
    }

    private void SetInstanceData(object oSender, object oParam)
    {
        this.m_InstanceDict.Clear ();

        Dictionary<string, InstanceResp> instanceDict = oParam as Dictionary<string, InstanceResp>;
        foreach (var item in instanceDict) {
            this.m_InstanceDict.Add(int.Parse(item.Key), item.Value);
        }

        SignalManager.Instance.DispatchSignal (SignalID.SelectView_SetView, null);
    }

    public void OnBackClicked()
    {
        UIManager.Instance.ShowViewByName (Constant.UI_Init);
    }


    //对外接口
    public Dictionary<int, InstanceResp> GetInstanceData()
    {
        return this.m_InstanceDict;
    }

    public void RequestWebInstanceData()
    {
        StartCoroutine (this.m_GetInstanceNet.Request ());
    }

    public void SelectInstance(int iID)
    {
        GlobalManager.Instance.StartInstance (iID, this.m_InstanceDict[iID]);
    }
}
