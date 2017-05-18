using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinimapView : MonoBehaviour
{
    private GameModel m_Model;

    public GameObject MapBg;
    public GameObject pf_Point;

    private Dictionary<int, GameObject> m_PointDict = new Dictionary<int, GameObject> ();
    private Vector2 m_v2RealMapSize;
    private Vector2 m_v2MinimapSize;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
		this.m_Model = this.gameObject.transform.parent.gameObject.GetComponent<GameModel> ();

        this.m_v2RealMapSize = GlobalManager.Instance.MapSize;
        this.m_v2MinimapSize = this.MapBg.GetComponent<RectTransform> ().sizeDelta;

        SignalManager.Instance.AddHandler (SignalID.ShipParamChanged, this.SetPointPos);
        SignalManager.Instance.AddHandler (SignalID.GameView_ControlChanged, this.SetPointShape);
    }

    void OnEnable()
    {
        if(this.m_PointDict.Count > 0)
        {
            foreach(var item in this.m_PointDict)
            {
                Destroy (item.Value);
            }
            this.m_PointDict.Clear ();
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    private void SetPointShape(object oSender, object oParam)
    {
        Debug.Log (oParam.ToString ());
        int iShipID = (int)oParam;
        //坐标颜色
        foreach(var item in this.m_PointDict)
        {
            if (iShipID == item.Key)
            {
                item.Value.GetComponent<Image> ().color = new Color (1, 0, 0);
            }
            else
            {
                item.Value.GetComponent<Image> ().color = new Color (0, 0, 1);
            }
        }
    }

    private void SetPointPos(object oSender, object oParam)
    {
        int iShipID = (int)oSender;
        GameObject oPoint = null;
        if (!this.m_PointDict.ContainsKey(iShipID))
        {
            oPoint = Instantiate (this.pf_Point);
            oPoint.transform.SetParent(this.MapBg.transform);
            this.m_PointDict.Add (iShipID, oPoint);
            //文本设置
            oPoint.transform.Find ("Text").gameObject.GetComponent<Text> ().text = iShipID.ToString ();
            //坐标颜色
            SetPointShape (null, this.m_Model.GetControlledShipID());
            
        }
        oPoint = this.m_PointDict[iShipID];
        //坐标位置
        SShipParam oShipParam = oParam as SShipParam;
        if(oShipParam == null)
        {
            return;
        }
        float fPosX = (float)oShipParam.posX / this.m_v2RealMapSize.x * this.m_v2MinimapSize.x;
        float fPosY = (float)oShipParam.posY / this.m_v2RealMapSize.y * this.m_v2MinimapSize.y;
        oPoint.transform.localPosition = new Vector3 (fPosX, fPosY, 0);

    }
}

