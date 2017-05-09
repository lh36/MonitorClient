using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectView : MonoBehaviour
{
    private SelectModel m_Model;

    private Button Btn_Back;

    public GameObject Scr_Content;
    public GameObject Pf_Instance;
    private List<GameObject> m_InstanceItemList = new List<GameObject> ();
    private Vector3 m_StartPoint;
    private float m_ItemHeight = 50;

    void Awake()
    {
        this.Btn_Back = transform.Find ("Btn_Quit").gameObject.GetComponent<Button> ();

        Btn_Back.onClick.AddListener (delegate {
            OnBackClick ();
        });

        SignalManager.Instance.AddHandler (SignalID.SelectView_SetView, this.SetScrollView);

        gameObject.AddComponent<SelectModel> ();
        this.m_Model = gameObject.GetComponent<SelectModel> ();
        this.m_Model.Init ();
    }

    void Start()
    {
        this.m_ItemHeight = this.Pf_Instance.GetComponent<RectTransform> ().sizeDelta.y;
    }
        

    /// <summary>
    /// 设置滑动列表
    /// </summary>
    private void SetScrollView(object oSender, object oParam)
    {
        ClearView ();

        Dictionary<int, InstanceResp> dInstanceData = this.m_Model.GetInstanceData ();
        List<int> IDList = new List<int> ();
        foreach(int iID in dInstanceData.Keys)
        {
            IDList.Add (iID);
        }
        IDList.Sort ();

        foreach (int iID in IDList) 
        {
            GameObject oItem = Instantiate (Pf_Instance) as GameObject;
            this.m_InstanceItemList.Add (oItem);
            InstanceResp oInstance = dInstanceData [iID];
            oItem.GetComponent<InsItem> ().SetItemView (oInstance.time, oInstance.name, oInstance.desp, oInstance.amount);
            oItem.transform.transform.SetParent (this.Scr_Content.transform);
            oItem.transform.localPosition = this.m_StartPoint;
            this.m_StartPoint += new Vector3 (0, -this.m_ItemHeight, 0);

            oItem.GetComponent<Button> ().onClick.AddListener (delegate {
                this.OnItemClicked(iID);
            });
        }
    }

    /// <summary>
    /// 滑动项点击
    /// </summary>
    private void OnItemClicked(int iID)
    {
        this.m_Model.SelectInstance (iID);
    }

    /// <summary>
    /// 回退按钮点击
    /// </summary>
    private void OnBackClick()
    {
        this.m_Model.OnBackClicked ();
    }

    /// <summary>
    /// Clears the view.
    /// </summary>
    private void ClearView()
    {
        this.m_StartPoint = new Vector3 (0, -this.m_ItemHeight/2, 0);

        if(this.m_InstanceItemList == null)
        {
            return;
        }

        foreach(GameObject oItem in this.m_InstanceItemList)
        {
            Destroy (oItem);
        }

        this.m_InstanceItemList.Clear ();
    }


}

